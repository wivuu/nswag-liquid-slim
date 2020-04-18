require('es6-promise').polyfill();
require('isomorphic-fetch');

import { WeatherForecast, setBaseUrl } from './output';

setBaseUrl("http://localhost:5000");

test('FETCH /WeatherForecast', async () => {
    const data = await WeatherForecast.get("/WeatherForecast");
    expect(data).not.toBeNull();
    expect(data[0]).not.toBeNull();

    const d = new Date(data[0].date);
    expect(d).toBeInstanceOf(Date);
});