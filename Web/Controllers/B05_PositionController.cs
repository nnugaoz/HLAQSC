using MyTool.Model;
using MyTool.MyEnum;
using QRCoder;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web.Mvc;
using Web.Models;
using Web.MyLib;

namespace Web.Controllers
{
    public class B05_PositionController : BaseController
    {
        private Model_Ret _model_ret = new Model_Ret();

        public ActionResult PageList()
        {
            return View();
        }

        public ActionResult Add()
        {
            T2_Position obj = new T2_Position();
            obj.Position_GetAll_ZTree(ref _model_ret.mrd01.dt);

            SelectOption obj_selectObj = new SelectOption();
            obj_selectObj.SelectType = "PositionType";
            obj_selectObj.Common_GetAll(ref _model_ret.mrd09.dt);

            ViewBag.Ret = _model_ret.Get_Ret();
            return View();
        }

        public ActionResult Edit(string ID)
        {
            T2_Position obj = new T2_Position();
            obj.ID = ID;
            obj.Position_GetOne(ref _model_ret.mrd02.dt);

            ViewBag.Ret = _model_ret.Get_Ret();
            return View();
        }

        public ActionResult Bind(string ID)
        {
            T2_Org obj_org = new T2_Org();
            obj_org.Position_GetAll_ZTree(ref _model_ret.mrd01.dt);

            T2_Position obj = new T2_Position();
            obj.ID = ID;
            obj.Position_GetOne(ref _model_ret.mrd02.dt);

            ViewBag.Ret = _model_ret.Get_Ret();
            return View();
        }

        public ActionResult QRCode(string code)
        {
            // 生成二维码的内容
            string strCode = code;
            QRCodeGenerator qrGenerator = new QRCoder.QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(strCode, QRCodeGenerator.ECCLevel.Q);
            QRCode qrcode = new QRCode(qrCodeData);

            // qrcode.GetGraphic 方法可参考最下发“补充说明”
            Bitmap qrCodeImage = qrcode.GetGraphic(5, Color.Black, Color.White, null, 15, 6, false);
            MemoryStream ms = new MemoryStream();
            qrCodeImage.Save(ms, ImageFormat.Jpeg);

            string filePath = ConfigurationManager.AppSettings["Log_Path"];
            filePath = filePath.Replace("Log", "QRCode") + code + ".jpeg";

            qrCodeImage.Save(filePath);

            return File(filePath, "image/jpeg");
        }
    }
}