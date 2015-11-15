using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Moq;
namespace GKSLab.Tests.MoqContext
{
    public class MoqHttpContext
    {
        public Mock<HttpContextBase> MockContext { get; set; }
        public Mock<HttpRequestBase> MockRequest { get; set; }
        public Mock<HttpResponseBase> MockResponse { get; set; }
        public Mock<HttpSessionStateBase> MockSession { get; set; }
        public Mock<HttpServerUtilityBase> MockServer { get; set; }
        public Mock<IPrincipal> MockUser { get; set; }
        public Mock<IIdentity> MockIdentity { get; set; }
        public Mock<HttpPostedFileBase> MockPostedFiles { get; set; }

        public HttpContextBase HttpContextBase { get; set; }
        public HttpRequestBase HttpRequestBase { get; set; }
        public HttpResponseBase HttpResponseBase { get; set; }

        public MoqHttpContext()
        {
            CreateBaseMocks();
            SetupNormalRequestValues();
            var test = this.HttpResponseBase.OutputStream;
            var test2 = this.HttpRequestBase.InputStream;

        }
        public MoqHttpContext CreateBaseMocks()
        {
            MockContext = new Mock<HttpContextBase>();
            MockRequest = new Mock<HttpRequestBase>();
            MockResponse = new Mock<HttpResponseBase>();
            MockSession = new Mock<HttpSessionStateBase>();
            MockServer = new Mock<HttpServerUtilityBase>();
            MockPostedFiles = new Mock<HttpPostedFileBase>();

            MockContext.Setup(ctx => ctx.Request).Returns(MockRequest.Object);
            MockContext.Setup(ctx => ctx.Response).Returns(MockResponse.Object);
            MockContext.Setup(ctx => ctx.Session).Returns(MockSession.Object);
            MockContext.Setup(ctx => ctx.Server).Returns(MockServer.Object);


            HttpContextBase = MockContext.Object;
            HttpRequestBase = MockRequest.Object;
            HttpResponseBase = MockResponse.Object;

            return this;
        }

        public MoqHttpContext SetupNormalRequestValues()
        {
            //Context.User
            var MockUser = new Mock<IPrincipal>();
            var MockIdentity = new Mock<IIdentity>();
            MockContext.Setup(context => context.User).Returns(MockUser.Object);
            MockUser.Setup(context => context.Identity).Returns(MockIdentity.Object);

            //Request
            MockRequest.SetupGet(request => request.InputStream).Returns(new MemoryStream());

            //Response
            MockResponse.SetupGet(response => response.OutputStream).Returns(new MemoryStream());
            HttpResponseBase = MockResponse.Object;
            HttpRequestBase = MockRequest.Object;

            return this;
        }

    }
}
