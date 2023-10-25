using FundooManager.IManager;
using FundooModel.Label;
using FundooModel.Notes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FundooApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabelController : ControllerBase
    {
        public readonly ILabelManager labelManager;
        public LabelController(ILabelManager labelManager)
        {
            this.labelManager = labelManager;
        }
        [HttpPost]
        [Route("AddLabel")]
        public async Task<ActionResult> AddLabel(Label label)
        {
            try
            {
                var result = await this.labelManager.AddLabel(label);
                if (result != 0)
                {
                    return this.Ok(new { Status = true, Message = "Label Added Successfully", Data = label });
                }
                return this.BadRequest(new { Status = false, Message = "Adding label Unsuccessful", Data = String.Empty });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { StatusCode = this.BadRequest(), Status = false, Message = ex.Message });
            }
        }
        [HttpPut]
        [Route("UpdateLabel")]
        public ActionResult UpdateLabel(Label label)
        {
            try
            {
                var result = this.labelManager.UpdateLabel(label);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Label Updates Successfully", Data = label });
                }
                return this.BadRequest(new { Status = false, Message = "Updating Label Unsuccessful" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
        [HttpGet]
        [Route("GetAllLabels")]
        public async Task<ActionResult> GetAllLabels(int userId)
        {
            try
            {
                var result = this.labelManager.GetAllLabels(userId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "All Labels Found", data = result });
                }
                return this.BadRequest(new { Status = false, Message = "No Labels Found" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
        [HttpPut]
        [Route("DeleteLabel")]
        public ActionResult DeleteLabel(int LabelId)
        {
            try
            {
                var result = this.labelManager.DeleteLabel(LabelId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Label Deleted Successfully" });
                }
                return this.BadRequest(new { Status = false, Message = "Deleting Label Unsuccessful" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
    }
}