# Weather
.NET Weather Application

##Solution keypoints
- created adapters to simplify interfaces and abstract from underlying packages being used
- favoured composition over inheritance to simplify design and support better re-use of services
- includes unit and integration tests using XUnit

##Requirements

#####Allow the user to choose which measurement unit they want results displayed in (mph, kph, Celsius, Fahrenheit).

Achieved by adding radio button controls in UI to select unit of measurement

#####Allow for more APIs to be easily added in the future.

Created an object to manage registration of service (WeatherServiceStore). Iterate over a collection of services and call common repository. Have made an assumption that future api calls will follow a similar url pattern 

#####Handle one or more of the APIs being down or being slow to respond.

Using RESTSharp have set a timeout of 15 seconds, if service either fails (HTTP code not 200) or times out we treat this as failed. AggregatorService will still create average for those results it has successfully retrieved.

#####Given temperatures of 10c from bbc and 68f from accuweather when searching then display either 15c or 59f (the average).

Use LINQ to get average of values and use UnitsNet package for conversions between units. 

#####Given wind speeds of 8kph from bbc and 10mph from accuweather when searching then display either 12kph or 7.5mph (the average).

As above with temperatures

#####Given an empty or null location when searching then do not make a request to the APIs.

Do this straight away in the AggregatorService to avoid any unneccesary processing.

#####Handle HTTP error codes from the APIs without displaying these to the user.

I have created an adapter for the RESTSharp interaction. In the adapter I haandle HTTP code 200, all other responses are treated as failures


##Things I would have done with more time:

- Improve test coverage - I ran out of time, but need to add further tests for WeatherAggregatorService and WeatherController.
- Improve usability of UI - use Ajax to get weather results, improve look and feel
- Threading - currently the solution is all synchronous - would look to leverage TPL to improve performance.
