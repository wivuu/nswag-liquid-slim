import * as api from './output';

var leads = await api.Leads.get(
    "/api/{partner}/Leads", 
    { partner: "Default" });

var profile = await api.Account.get("/api/User/Profile");

var users = await api.Users.get("/api/{partner}/Users", { partner: "Default" });