import * as api from './output';

const x = await api.Leads.get("/api/{partner}/Leads", { partner: "Default" });