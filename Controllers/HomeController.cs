using Microsoft.AspNetCore.Mvc;
using System;

namespace Calculator.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home - แสดงหน้า Calculator
        public IActionResult Index()
        {
            return View();
        }

        // POST: Home/Calculate - คำนวณผลลัพธ์
        [HttpPost]
        public IActionResult Calculate([FromForm] double num1, [FromForm] double num2, [FromForm] string operatorType)
        {
            try
            {
                double result = 0;
                bool success = true;
                string message = "";

                // ตรวจสอบประเภทการคำนวณ
                switch (operatorType?.ToLower())
                {
                    case "add":
                        result = num1 + num2;
                        break;
                    case "subtract":
                        result = num1 - num2;
                        break;
                    case "multiply":
                        result = num1 * num2;
                        break;
                    case "divide":
                        if (num2 == 0)
                        {
                            success = false;
                            message = "ไม่สามารถหารด้วย 0 ได้";
                        }
                        else
                        {
                            result = num1 / num2;
                        }
                        break;
                    default:
                        success = false;
                        message = "กรุณาเลือกประเภทการคำนวณ";
                        break;
                }

                // ส่งผลลัพธ์กลับไปในรูปแบบ JSON
                return Json(new
                {
                    success = success,
                    result = success ? Math.Round(result, 6) : 0, // ปัดเศษทศนิยม 6 ตำแหน่ง
                    message = message
                });
            }
            catch (Exception ex)
            {
                // จัดการกรณีเกิดข้อผิดพลาด
                return Json(new
                {
                    success = false,
                    result = 0,
                    message = "เกิดข้อผิดพลาดในระบบ: " + ex.Message
                });
            }
        }

        // POST: Home/CalculateJson - สำหรับ Fetch API ที่ส่ง JSON
        [HttpPost]
        public IActionResult CalculateJson([FromBody] CalculatorModel model)
        {
            try
            {
                double result = 0;
                bool success = true;
                string message = "";

                // ตรวจสอบประเภทการคำนวณ
                switch (model.Operator?.ToLower())
                {
                    case "add":
                        result = model.Num1 + model.Num2;
                        break;
                    case "subtract":
                        result = model.Num1 - model.Num2;
                        break;
                    case "multiply":
                        result = model.Num1 * model.Num2;
                        break;
                    case "divide":
                        if (model.Num2 == 0)
                        {
                            success = false;
                            message = "ไม่สามารถหารด้วย 0 ได้";
                        }
                        else
                        {
                            result = model.Num1 / model.Num2;
                        }
                        break;
                    default:
                        success = false;
                        message = "กรุณาเลือกประเภทการคำนวณ";
                        break;
                }

                return Json(new
                {
                    success = success,
                    result = success ? Math.Round(result, 6) : 0,
                    message = message
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    result = 0,
                    message = "เกิดข้อผิดพลาดในระบบ: " + ex.Message
                });
            }
        }
    }

    // Model class สำหรับรับข้อมูลจาก Frontend
    public class CalculatorModel
    {
        public double Num1 { get; set; }
        public double Num2 { get; set; }
        public string? Operator { get; set; } // เพิ่ม ? เพื่อทำให้เป็น nullable
    }

    // ผลลัพธ์ที่ส่งกลับไป Frontend
    public class CalculatorResult
    {
        public bool Success { get; set; }
        public double Result { get; set; }
        public string? Message { get; set; } // เพิ่ม ? เพื่อทำให้เป็น nullable
    }
}