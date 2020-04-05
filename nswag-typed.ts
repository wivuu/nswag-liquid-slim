/** Constants */
const jsonRequestInfo : RequestInit = {
    headers: {
        "Content-Type": "application/json",
        "Accept": "application/json"
    }
}

const defaultRequestInfo : RequestInit = {
    headers: {
        "Accept": "application/json"
    }
}


/** Generated Add Post description goes here */
async function apiPost(info: "/api/posts", params: null, init: TypedRequestInit<AddPostModel>): Promise<AddPostReturnModel>;
/** Generated Add User description goes here */
async function apiPost(info: "/api/users", params: null, init: TypedRequestInit<AddUserModel>): Promise<AddUserReturnModel>;

/** IMPL of POST */
function apiPost(info: string, params?: object, init?: TypedRequestInit) {
    return apiJson("POST", info, init, params, getModel(init));
}


/** Delete a user */
async function apiDelete(info: "/api/users", init: TypedRequestInit<DeleteUserModel>): Promise<Response>;

/** IMPL of DELETE */
function apiDelete(info: string, init?: TypedRequestInit) {
    return apiBase("DELETE", info, init, getModel(init));
}


/** Get all users (matching filter) */
async function apiGet(info: "/api/users", init?: TypedRequestInit<{ filter: string }>): Promise<GetUserModel[]>;

/** IMPL of GET */
function apiGet(info: string, init?: TypedRequestInit) {
    return apiBase("GET", info, init, getModel(init));
}


/** IMPL of PUT */
function apiPut(info: string, params?: object, init?: TypedRequestInit) {
    return apiJson("PUT", info, init, params, getModel(init));
}


/** Helpers */
async function apiJson(method: HttpMethodsWithBody, info: string, init: RequestInit, params?: object, body?: object) {
    return apiBase(method, info, init, params, !!body ? JSON.stringify(body) : undefined);
}

async function apiForm(method: HttpMethodsWithBody, info: string, init: RequestInit, params?: object, body?: object) {
    return apiBase(method, info, init, params, !!body ? getFormData(body) : undefined);
}

async function apiBase(method: HttpMethods, info: string, init: RequestInit, params?: object, body?: string | FormData) {
    info     = !!params ? formatParams(info, params) : info;
    const qs = !!params ? formatQueryString(params)  : "";

    const response = await fetch(
        info + qs,
        { 
            method, 
            ...body instanceof FormData ? defaultRequestInfo : jsonRequestInfo,
            ...init, 
            body
        });

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
apiPost("/api/users", null, { name: "Ted", age: 25 });
apiPost("/api/posts", null, { body: "This is a test" });
apiDelete("/api/users", { id: 5 });
apiGet("/api/users");

/** Types */
type HttpMethodsWithBody = "PUT" | "POST" | "PATCH";
type HttpMethods = "HEAD" | "GET" | "DELETE" | HttpMethodsWithBody;

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
