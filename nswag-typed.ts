/** Constants */
const jsonRequestInfo : RequestInit = {
    headers: {
        "Content-Type": "application/json",
        "Accept": "application/json"
    }
}

const formRequestInfo : RequestInit = {
    headers: {
        "Accept": "application/json"
    }
}


/** Generated Add Post description goes here */
async function apiPost(info: "/api/posts", init: TypedRequestInit<AddPostModel>): Promise<AddPostReturnModel>;
/** Generated Add User description goes here */
async function apiPost(info: "/api/users", init: TypedRequestInit<AddUserModel>): Promise<AddUserReturnModel>;

/** IMPL of POST */
function apiPost(info: string, params?: object, init?: TypedRequestInit) {
    return api("POST", info, init, params, getModel(init));
}


/** Delete a user */
async function apiDelete(info: "/api/users", init: TypedRequestInit<DeleteUserModel>): Promise<Response>;

/** IMPL of DELETE */
function apiDelete(info: string, init?: TypedRequestInit) {
    return api("DELETE", info, init, getModel(init));
}


/** Get all users (matching filter) */
async function apiGet(info: "/api/users", init?: TypedRequestInit<{ filter: string }>): Promise<GetUserModel[]>;

/** IMPL of GET */
function apiGet(info: string, init?: TypedRequestInit) {
    return api("GET", info, init, getModel(init));
}

/** IMPL of PUT */
function apiPUT(info: string, init?: TypedRequestInit) {
}

/** Helpers */
async function api(method: string, info: string, init: RequestInit, params?: object, body?: object) {
    info     = !!params ? formatParams(info, params) : info;
    const qs = !!params ? formatQueryString(params)  : "";

    const response = await fetch(
        info + qs,
        { 
            method, 
            ...jsonRequestInfo, 
            ...init, 
            body: !!body ? JSON.stringify(body) : undefined 
        }
    );

    return await response.json();
}

async function apiForm(method: string, info: string, init: RequestInit, params?: object, body?: object) {
    info     = !!params ? formatParams(info, params) : info;
    const qs = !!params ? formatQueryString(params)  : "";

    const response = await fetch(
        info + qs,
        { 
            method, 
            ...formRequestInfo, 
            ...init, 
            body: !!body ? getFormData(body) : undefined
        }
    );

    return await response.json();
}

/** Return 'model' or 'init' object, but if 'model' is inside 'init', removes 'model' property */
function getModel(init: { model?: any }) {
    if ("model" in init) {
        const model = init.model;
        delete init.model;
        return model;
    }

    return init;
}

const pattern = /\/\{\w+\}/g;

/** Format params string, removing {template} values and any matches from the params object */
function formatParams<T extends object>(info: string, model: T) {
    for (const m of info.match(pattern)) {
        const key   = m.slice(2, -1);
        const value = model[key];
        
        if (value !== null && value !== undefined) {
            info = info.replace(m, "/" + encodeURIComponent("" + value));
            delete model[key];
        }
        else
            info = info.replace(m, "");
    }

    return info;
}

/** Return formdata */
function getFormData(model: object) {
    const data = new FormData();

    for (const key of Object.keys(model)) {
        const value = model[key];

        if (value !== null && value !== undefined)
            data.append(key, value.toString());
    }
    
    return data;
}


/** Format a querystring from an object */
function formatQueryString<T extends object>(model: T) {
    const keys = Object.keys(model);
    let qs     = "";

    for (let i = 0; i < keys.length; ++i) {
        const key   = keys[i], 
              value = model[key];
        
        qs += (i === 0 ? "?" : "&") +
              encodeURIComponent(key) + "=" + encodeURIComponent("" + value);
    }

    return qs;
}

/** Example Usage */
apiPost("/api/users", { name: "Ted", age: 25 });
apiPost("/api/posts", { body: "This is a test" });
apiDelete("/api/users", { id: 5 });
apiGet("/api/users");

/** Types */

type AddPostModel = {
    subject?: string
    body: string
}

type AddPostReturnModel = {
    id: number
}

type AddUserModel = {
    name: string
    age: number
}

type AddUserReturnModel = {
    id: number
}

type DeleteUserModel = { 
    id: number
};

type GetUserModel = {
    id: number
    name: string
    age: string
}

type TypedRequestInit<T = any> = T | ({ model: any } & RequestInit);

type TypedResponse<T = any> = T;
