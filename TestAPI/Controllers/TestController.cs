using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace istemci1.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [Authorize (Policy = "Istemci1Guvenligi")]
        [HttpGet("istemci1")]
        public ActionResult GetPublicData()
        {
            return Ok("Bu veri sadece İSTEMCİ1 clienti tarafından erişilebilir");
        }
        [Authorize (Policy = "JsGuvenligi")]
        [HttpGet("js")]
        public ActionResult GetJsData()
        {
            return Ok("Bu veri sadece JS clienti tarafından erişilebilir");
        }

        [Authorize(Policy = "UserYetkiGuvenligi")]
        [HttpGet("user")]
        public ActionResult GetUserData()
        {
            return Ok("Bu veri sadece OKUMA yetkisine sahip KULLANICILAR tarafından erişilebilir");
        }


        [Authorize(Policy = "AdminYetkiGuvenligi")]
        [HttpGet("admin")]
        public ActionResult UpdateAdminData()
        {
            return Ok("Bu veri sadece GÜNCELLEME yetkisine sahip KULLANICILAR tarafından erişilebilir");
        }

        [Authorize(Policy = "YetkiliClientGuvenligi")]
        [HttpGet("yetkiliclient")]
        public ActionResult GetYetkiliClientData()
        {
            return Ok("Bu veri sadece OKUMA yetkisine sahip CLIENT'lar tarafından erişilebilir");
        }
    }
    
}