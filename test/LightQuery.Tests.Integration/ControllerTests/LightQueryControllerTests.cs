using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace LightQuery.Tests.Integration.ControllerTests
{
    public class LightQueryControllerTests : ControllerTestBase
    {
        [Fact]
        public async Task SortById()
        {
            var url = "LightQuery?sort=id";
            var actualResponse = await GetResponse<List<User>>(url);
            for (var i = 1; i < actualResponse.Count; i++)
            {
                var previousValueIsSmaller = actualResponse[i].Id > actualResponse[i-1].Id;
                Assert.True(previousValueIsSmaller);
            }
        }

        [Fact]
        public async Task SortByIdDescending()
        {
            var url = "LightQuery?sort=id desc";
            var actualResponse = await GetResponse<List<User>>(url);
            for (var i = 1; i < actualResponse.Count; i++)
            {
                var previousValueIsSmaller = actualResponse[i].Id > actualResponse[i-1].Id;
                Assert.False(previousValueIsSmaller);
            }
        }

        [Fact]
        public async Task SortByUserName()
        {
            var url = "LightQuery?sort=userName";
            var actualResponse = await GetResponse<List<User>>(url);
            for (var i = 1; i < actualResponse.Count; i++)
            {
                var previousValueIsSmaller = actualResponse[i].UserName.CompareTo(actualResponse[i - 1].UserName) > 0;
                Assert.True(previousValueIsSmaller);
            }
        }

        [Fact]
        public async Task SortByUserNameDescending()
        {
            var url = "LightQuery?sort=userName desc";
            var actualResponse = await GetResponse<List<User>>(url);
            for (var i = 1; i < actualResponse.Count; i++)
            {
                var previousValueIsSmaller = actualResponse[i].UserName.CompareTo(actualResponse[i - 1].UserName) > 0;
                Assert.False(previousValueIsSmaller);
            }
        }

        [Fact]
        public async Task SortByEmail()
        {
            var url = "LightQuery?sort=email";
            var actualResponse = await GetResponse<List<User>>(url);
            for (var i = 1; i < actualResponse.Count; i++)
            {
                var previousValueIsSmaller = actualResponse[i].Email.CompareTo(actualResponse[i - 1].Email) > 0;
                Assert.True(previousValueIsSmaller);
            }
        }

        [Fact]
        public async Task SortByEmailDescending()
        {
            var url = "LightQuery?sort=email desc";
            var actualResponse = await GetResponse<List<User>>(url);
            for (var i = 1; i < actualResponse.Count; i++)
            {
                var previousValueIsSmaller = actualResponse[i].Email.CompareTo(actualResponse[i - 1].Email) > 0;
                Assert.False(previousValueIsSmaller);
            }
        }

        [Fact]
        public async Task DontSortWithoutSortParameter()
        {
            var url = "LightQuery";
            var response1 = await GetResponse<List<User>>(url);
            var response2 = await GetResponse<List<User>>(url);
            var response3 = await GetResponse<List<User>>(url);
            var response4 = await GetResponse<List<User>>(url);
            Func<IEnumerable<User>, string> aggregateEmails = users =>
                users
                    .Select(u => u.Email)
                    .Aggregate((current, next) => current + next);
            var firstAggregate = aggregateEmails(response1) + aggregateEmails(response2);
            var secondAggregate = aggregateEmails(response3) + aggregateEmails(response4);
            Assert.NotEqual(firstAggregate, secondAggregate);
        }

        [Fact]
        public async Task DontSortWithInvalidSortParameter()
        {
            var url = "LightQuery?sort=firstName";
            var response1 = await GetResponse<List<User>>(url);
            var response2 = await GetResponse<List<User>>(url);
            var response3 = await GetResponse<List<User>>(url);
            var response4 = await GetResponse<List<User>>(url);
            Func<IEnumerable<User>, string> aggregateEmails = users =>
                users
                    .Select(u => u.Email)
                    .Aggregate((current, next) => current + next);
            var firstAggregate = aggregateEmails(response1) + aggregateEmails(response2);
            var secondAggregate = aggregateEmails(response3) + aggregateEmails(response4);
            Assert.NotEqual(firstAggregate, secondAggregate);
        }
    }
}