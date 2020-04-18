require('es6-promise').polyfill();
require('isomorphic-fetch');

import { WeatherForecast, setBaseUrl } from './output';

setBaseUrl("http://localhost:5000");

test('GET /WeatherForecast', async () => {
    const data = await WeatherForecast.get("/WeatherForecast");
    expect(data).not.toBeNull();
    expect(data[0]).not.toBeNull();

    const d = new Date(data[0].date);
    expect(d).toBeInstanceOf(Date);
});

test('POST /WeatherForecast', async () => {
    await WeatherForecast.post("/WeatherForecast", null, { 
        date: new Date(),
        temperatureC: .5,
        temperatureF: .5,
        summary: "OK"
    });
});