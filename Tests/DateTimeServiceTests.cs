using System;
using Xunit;
using musichino.Services;

namespace Tests
{
    public class DateTimeServiceTests
    {
        [Fact]
        public void UnixTimeToDateTimeTest()
        {
            Assert.Equal(new DateTime(1970, 1, 1), DateTimeService.UnixTimeToDateTime(0));
            Assert.Equal(new DateTime(2013, 11, 5), DateTimeService.UnixTimeToDateTime(1383609600));
            Assert.Equal(new DateTime(1984, 9, 12), DateTimeService.UnixTimeToDateTime(463795200));
            Assert.Equal(new DateTime(2002, 2, 20), DateTimeService.UnixTimeToDateTime(1014163200));
            Assert.Equal(new DateTime(1996, 8, 15), DateTimeService.UnixTimeToDateTime(840067200));
            Assert.Equal(new DateTime(1989, 3, 22), DateTimeService.UnixTimeToDateTime(606528000));
        }
    }
}