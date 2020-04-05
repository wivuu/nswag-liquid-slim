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

function getFormData(model: object) {
    const data = new FormData();

    for (const key of Object.keys(model)) {
        const value = model[key];

        if (value !== null && value !== undefined)
            data.append(key, value.toString());
    }

    return data;
}

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

enum APIRequestType {
    None,
    Json,
    Form,
}

type TypedRequestInit<T = any> = T | ({ model: any } & RequestInit);

type TypedResponse<T = any> = T;

abstract class APIRequest<T extends object = object> {
    init: RequestInit;

    constructor(
        protected type: APIRequestType,
        protected method: HttpMethods,
        protected body?: T
    ) {}

    setRequestInit(init: RequestInit) {
        this.init = init;
        return this;
    }

    toRequest(): RequestInit {
        return {
            // TODO:
            method: this.method,
            ...this.type === APIRequestType.Json ? jsonRequestInfo : defaultRequestInfo,
            ...this.init,
            body: this.type === APIRequestType.Json ? JSON.stringify(this.body) : getFormData(this.body)
        };
    }
}

class JsonAPIRequest<T extends object> extends APIRequest<T> {

    constructor(
        protected method: HttpMethodsWithBody,
        protected body: T
    ) {
        super(APIRequestType.Json, method, body);
    }
}

new JsonAPIRequest("POST", { x: 5 }).toRequest();

export {}