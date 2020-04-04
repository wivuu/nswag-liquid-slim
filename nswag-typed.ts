/** Constants */
const defaultRequestInfo : RequestInit = {
    headers: {
        "Content-Type": "application/json"
    }
}


/** Generated Add Post description goes here */
async function fetchPost(info: "/api/posts", init: TypedRequestInit<AddPostModel>): Promise<AddPostReturnModel>;
/** Generated Add User description goes here */
async function fetchPost(info: "/api/users", init: TypedRequestInit<AddUserModel>): Promise<AddUserReturnModel>;

/** IMPL of POST */
function fetchPost(info: string, init?: TypedRequestInit) {
    return fetchInternal("POST", info, init, {
        body: modelOrInit(init)
    });
}


/** Delete a user */
async function fetchDelete(info: "/api/users", init: TypedRequestInit<DeleteUserModel>): Promise<Response>;

/** IMPL of DELETE */
function fetchDelete(info: string, init?: TypedRequestInit) {
    return fetchInternal("DELETE", info, init, {
        qs: modelOrInit(init)
    });
}


/** Get all users (matching filter) */
async function fetchGet(info: "/api/users", init?: TypedRequestInit<{ filter: string }>): Promise<GetUserModel[]>;

/** IMPL of GET */
function fetchGet(info: string, init?: TypedRequestInit) {
    return fetchInternal("GET", info, init, {
        qs: modelOrInit(init)
    });
}


/** Helpers */
async function fetchInternal(method: string, info: string, init: RequestInit, include: { qs?: object, params?: object, body?: object }) {
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
function modelOrInit(init: { model?: any }) {
    const modelInInit = "model" in init;
    const model       = modelInInit ? init.model : init;
    
    if (modelInInit)
        delete init.model;

    return model;
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
fetchPost("/api/users", { name: "Ted", age: 25 });
fetchPost("/api/posts", { body: "This is a test" });
fetchDelete("/api/users", { id: 5 });
fetchGet("/api/users");

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
