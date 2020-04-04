/** Constants */
const defaultRequestInfo : RequestInit = {
    headers: {
        "Content-Type": "application/json"
    }
}


/** Generated Add Post description goes here */
async function apiPost(info: "/api/posts", init: TypedRequestInit<AddPostModel>): Promise<AddPostReturnModel>;
/** Generated Add User description goes here */
async function apiPost(info: "/api/users", init: TypedRequestInit<AddUserModel>): Promise<AddUserReturnModel>;

/** IMPL of POST */
function apiPost(info: string, init?: TypedRequestInit) {
    return api("POST", info, init, {
        body: getModel(init)
    });
}


/** Delete a user */
async function apiDelete(info: "/api/users", init: TypedRequestInit<DeleteUserModel>): Promise<Response>;

/** IMPL of DELETE */
function apiDelete(info: string, init?: TypedRequestInit) {
    return api("DELETE", info, init, {
        qs: getModel(init)
    });
}


/** Get all users (matching filter) */
async function apiGet(info: "/api/users", init?: TypedRequestInit<{ filter: string }>): Promise<GetUserModel[]>;

/** IMPL of GET */
function apiGet(info: string, init?: TypedRequestInit) {
    return api("GET", info, init, {
        qs: getModel(init)
    });
}


/** Helpers */
async function api(method: string, info: string, init: RequestInit, include: { qs?: object, params?: object, body?: object }) {
    const qs    = !!include.qs ? formatQueryString(include.qs) : "";
    const body  = !!include.body ? JSON.stringify(include.body) : undefined;
    info        = !!include.params ? formatParams(info, include.params) : info;

    const response = await fetch(
        info + qs,
        { method, ...defaultRequestInfo, ...init, body }
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

/** Format params string, removing {template} values */
function formatParams<T extends object>(info: string, model: T) {
    for (const key of Object.keys(model)) {
        const pattern = new RegExp("{" + key + "}", "g");
        info          = info.replace(pattern, model[key]);
    }

    return info;
}

/** Format a querystring from an object */
function formatQueryString<T extends object>(model: T) {
    const keys = Object.keys(model)
    let qs     = "";

    for (let i = 0; i < keys.length; ++i) {
        const key   = keys[i], 
              value = model[key];
        
        qs += (i === 0 ? "?" : "&") +
              encodeURIComponent(key) + "=" + encodeURIComponent(value);
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
