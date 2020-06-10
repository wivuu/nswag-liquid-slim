require('es6-promise').polyfill();
require('isomorphic-fetch');

import { WeatherForecast, setBaseUrl, APIError } from './output';

setBaseUrl("http://localhost:5000");

describe("WeatherForecast", () => {
    test('GET /WeatherForecast', async () => {
        try {
            const data = await WeatherForecast.get({ 
                fromDate: new Date(),
            });
            expect(data).not.toBeNull();
            expect(data[0]).not.toBeNull();

            const d = new Date(data[0].date);
            expect(d).toBeInstanceOf(Date);
        }
        catch (e) {
            if (e instanceof APIError) {
                console.error(e.message);
            }
            fail(e);
        }
    });

    test('POST /WeatherForecast', async () => {
        try {
            await WeatherForecast.addForecast(null, { 
                date: new Date(),
                temperatureC: .5,
                summary: "OK"
            });

            fail("Should not have gotten here");
        }
        catch (err) {
            expect(err.data).toBeInstanceOf(Object);
            expect(err.data.errors).toBeTruthy();
        }
    });

    test('PUT /WeatherForecast', async () => {
        const forecast = await WeatherForecast.updateForecast(null, { 
            date: new Date(),
            temperatureC: 5,
            summary: "OK"
        });

        expect(forecast).not.toBeNull();
        expect(forecast.temperatureC).toBe(5);
    });
});