using System;
using System.Collections.Generic;
using FluentAssertions;
using Rhino.Mocks;
using Weather.Service;
using Weather.Service.Core.Adapters;
using Weather.Service.Models;
using Weather.Service.Repository;
using Xunit;
using Container = Weather.Test.Core.Container;

namespace Weather.Test.Unit
{
    public class WeatherRepositoryTest
    {
        [Fact]
        public void GetDependenciesCalledWithCorrectParamsTest()
        {
            //Arrange
            const string location = "TestLocation";
            var container = new Container();
            var stubRestClient = MockRepository.GenerateStub<IRestClientAdapter>();
            var stubJsonConverter = MockRepository.GenerateStub<IJsonConvertAdapter>();

            container.RegisterInstance(stubRestClient);
            container.RegisterInstance(stubJsonConverter);
            container.Register<IWeatherRepository, WeatherRepository>();

            var weatherRepository = container.Get<IWeatherRepository>();
            var restClientResponse = new RestClientResponse()
            {
                Content = "TestContent"
            };
            var weatherServiceDetail = new WeatherServiceDetail()
            {
                Resource = "TestResource",
                ServiceUrl = "TestUrl"
            };         
            var urlSegment = new Dictionary<string, string> {{"location", location}};

            stubRestClient.Stub(x => x.Get(Arg<string>.Is.Anything, Arg<string>.Is.Anything, Arg<Dictionary<string, string>>.Is.Anything))
                .Return(restClientResponse);

            //Act
            weatherRepository.Get(weatherServiceDetail, location);

            //Assert
            stubRestClient.AssertWasCalled(x => x.Get(weatherServiceDetail.ServiceUrl, weatherServiceDetail.Resource, urlSegment));
            stubJsonConverter.AssertWasCalled(x => x.DeserializeObject<WeatherServiceResponse>(restClientResponse.Content));
        }

        [Fact]
        public void GetServiceFailedExceptionThrownTest()
        {
            //Arrange
            var container = new Container();
            var stubRestClient = MockRepository.GenerateStub<IRestClientAdapter>();
            var stubJsonConverter = MockRepository.GenerateStub<IJsonConvertAdapter>();

            container.RegisterInstance(stubRestClient);
            container.RegisterInstance(stubJsonConverter);
            container.Register<IWeatherRepository, WeatherRepository>();

            var weatherRepository = container.Get<IWeatherRepository>();
            var restClientResponse = new RestClientResponse()
            {
                Content = "TestContent",
                ResponseStatus = RestResponseStatus.Failure
            };
            var weatherServiceDetail = new WeatherServiceDetail()
            {
                Resource = "TestResource",
                ServiceUrl = "TestUrl"
            };
            const string location = "TestLocation";

            stubRestClient.Stub(x => x.Get(Arg<string>.Is.Anything, Arg<string>.Is.Anything, Arg<Dictionary<string, string>>.Is.Anything))
                .Return(restClientResponse);

            //Act
            Action action = () => weatherRepository.Get(weatherServiceDetail, location);

            //Assert
            action.ShouldThrow<WeatherServiceException>()
                .WithMessage("Service failed");
        }
    }
}
