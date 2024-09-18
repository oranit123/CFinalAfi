using DamoFullPrj.Models;
using FinalWebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace DamoFullPrj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        #region User Actions

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Parent))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Login([FromBody] LoginData ld)
        {
            try
            {
                Parent user = DBServices.Login(ld.emailLogin, ld.passLogin);
                if (user != null)
                {
                    return Ok(user);
                }
                else
                {
                    return NotFound($"User with email = {ld.emailLogin} and password not found!");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

  

        [HttpPost("parent/add")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Parent))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddUser([FromBody] Parent user)
        {
            try
            {
                bool isAdded = DBServices.AddParent(user);
                if (isAdded)
                {
                    return CreatedAtAction(nameof(GetParentById), new { id = user.idParent }, user);
                }
                else
                {
                    return BadRequest("Failed to add the user.");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("parent/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Parent))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetParentById(string id)
        {
            try
            {
                Parent parent = DBServices.GetParentById(id);
                if (parent != null)
                {
                    return Ok(parent);
                }
                return NotFound($"Parent with id = {id} not found.");
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }

        [HttpGet("parent/email/{email}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Parent))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetParentByEmail(string email)
        {
            try
            {
                Parent parent = DBServices.GetParentByEmail(email);
                if (parent != null)
                {
                    return Ok(parent);
                }
                return NotFound($"Parent with email = {email} not found.");
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }


        [HttpPut("parent/edit")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult EditUser([FromBody] Parent user)
        {
            try
            {
                bool isEdited = DBServices.EditParent(user);
                if (isEdited)
                {
                    return Ok("User updated successfully.");
                }
                else
                {
                    return BadRequest("Failed to update the user.");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("parent/delete/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteUser(string id)
        {
            try
            {
                bool isDeleted = DBServices.DeleteParent(id);
                if (isDeleted)
                {
                    return Ok("User deleted successfully.");
                }
                else
                {
                    return NotFound($"User with id = {id} not found.");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("parent/all")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Parent>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetAllUsers()
        {
            try
            {
                List<Parent> users = DBServices.GetAllParents();
                return Ok(users);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        #endregion

        #region Baby Actions

        [HttpPost("baby/add")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Baby))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddBaby([FromBody] Baby baby)
        {
            try
            {
                bool isAdded = DBServices.AddChild(baby);
                if (isAdded)
                {
                    return CreatedAtAction(nameof(GetBabyById), new { id = baby.idChild}, baby);
                }
                else
                {
                    return BadRequest("Failed to add the baby.");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("baby/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Baby))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetBabyById(string id)
        {
            try
            {
                Baby baby = DBServices.GetBabyById(id);
                if (baby != null)
                {
                    return Ok(baby);
                }
                else
                {
                    return NotFound($"Baby with id = {id} not found.");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("baby/edit")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult EditBaby([FromBody] Baby baby)
        {
            try
            {
                bool isEdited = DBServices.EditChild(baby);
                if (isEdited)
                {
                    return Ok("Baby updated successfully.");
                }
                else
                {
                    return BadRequest("Failed to update the baby.");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("baby/delete/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteBaby(string id)
        {
            try
            {
                bool isDeleted = DBServices.DeleteChild(id);
                if (isDeleted)
                {
                    return Ok("Baby deleted successfully.");
                }
                else
                {
                    return NotFound($"Baby with id = {id} not found.");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("baby/all")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Baby>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetAllBabies()
        {
            try
            {
                List<Baby> babies = DBServices.GetAllChildren();
                return Ok(babies);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        #region Baby Actions

        [HttpGet("baby/parent/{parentId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Baby>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetChildrenByParentId(string parentId)
        {
            if (string.IsNullOrWhiteSpace(parentId))
            {
                return BadRequest("Invalid parent ID.");
            }

            try
            {
                List<Baby> children = DBServices.GetChildrenByParentId(parentId);
                if (children.Any())
                {
                    return Ok(children);
                }
                else
                {
                    return NotFound($"No children found for parent with id = {parentId}.");
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving children.");
            }
        }

        #endregion

        #endregion

        #region Feed Actions

        [HttpPost("feed/add")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Feed))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddFeed([FromBody] Feed feed)
        {
            try
            {
                bool isAdded = DBServices.AddFeed(feed);
                if (isAdded)
                {
                    return CreatedAtAction(nameof(GetFeedById), new { id = feed.idBaby }, feed);
                }
                else
                {
                    return BadRequest("Failed to add the feed.");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("feed/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Feed))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetFeedById(string id, DateTime timeDate)
        {
            try
            {
                Feed feed = DBServices.GetFeedById(id, timeDate);
                if (feed != null)
                {
                    return Ok(feed);
                }
                else
                {
                    return NotFound($"Feed with id = {id} not found.");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("feed/edit")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult EditFeed([FromBody] Feed feed)
        {
            try
            {
                bool isEdited = DBServices.EditFeed(feed);
                if (isEdited)
                {
                    return Ok("Feed updated successfully.");
                }
                else
                {
                    return BadRequest("Failed to update the feed.");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("feed/delete/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteFeed(string id, DateTime timeDate)
        {
            try
            {
                bool isDeleted = DBServices.DeleteFeed(id,timeDate);
                if (isDeleted)
                {
                    return Ok("Feed deleted successfully.");
                }
                else
                {
                    return NotFound($"Feed with id = {id} not found.");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("feed/all")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Feed>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetAllFeeds()
        {
            try
            {
                List<Feed> feeds = DBServices.GetAllFeeds();
                return Ok(feeds);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        #endregion

        #region Sleep Actions

        [HttpPost("sleep/add")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Sleep))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddSleep([FromBody] Sleep sleep)
        {
            try
            {
                bool isAdded = DBServices.AddSleep(sleep);
                if (isAdded)
                {
                    return CreatedAtAction(nameof(GetSleepById), new { id = sleep.idChild }, sleep);
                }
                else
                {
                    return BadRequest("Failed to add the sleep.");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("sleep/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Sleep))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetSleepById(string id, DateTime timeDate)
        {
            try
            {
                Sleep sleep = DBServices.GetSleepById(id, timeDate);
                if (sleep != null)
                {
                    return Ok(sleep);
                }
                else
                {
                    return NotFound($"Sleep with id = {id} not found.");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("sleep/edit")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult EditSleep([FromBody] Sleep sleep)
        {
            try
            {
                bool isEdited = DBServices.EditSleep(sleep);
                if (isEdited)
                {
                    return Ok("Sleep updated successfully.");
                }
                else
                {
                    return BadRequest("Failed to update the sleep.");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("sleep/delete/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteSleep(string id, DateTime timeDate)
        {
            try
            {
                bool isDeleted = DBServices.DeleteSleep(id, timeDate);
                if (isDeleted)
                {
                    return Ok("Sleep deleted successfully.");
                }
                else
                {
                    return NotFound($"Sleep with id = {id} not found.");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("sleep/all")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Sleep>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetAllSleeps()
        {
            try
            {
                List<Sleep> sleeps = DBServices.GetAllSleepRecords();
                return Ok(sleeps);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        #endregion

        #region Vaccination Actions

        [HttpPost("vaccination/add")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Vaccination))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddVaccination([FromBody] Vaccination vaccination)
        {
            try
            {
                bool isAdded = DBServices.AddVaccination(vaccination);
                if (isAdded)
                {
                    return CreatedAtAction(nameof(GetVaccinationById), new { id = vaccination.idChild, date = vaccination.timeDate }, vaccination);
                }
                else
                {
                    return BadRequest("Failed to add the vaccination.");
                }
            }
            catch (Exception e)
            {
           
                return BadRequest(e.Message);
            }
        }

        [HttpGet("vaccination/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Vaccination))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetVaccinationById(string id, [FromQuery] DateTime date)
        {
            try
            {
                Vaccination vaccination = DBServices.GetVaccinationById(id, date);
                if (vaccination != null)
                {
                    return Ok(vaccination);
                }
                else
                {
                    return NotFound($"Vaccination record with id = {id} and date = {date} not found.");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("vaccination/edit")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult EditVaccination([FromBody] Vaccination vaccination)
        {
            try
            {
                bool isEdited = DBServices.EditVaccination(vaccination);
                if (isEdited)
                {
                    return Ok("Vaccination updated successfully.");
                }
                else
                {
                    return BadRequest("Failed to update the vaccination.");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("vaccination/delete/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteVaccination(string id, [FromQuery] DateTime date)
        {
            try
            {
                bool isDeleted = DBServices.DeleteVaccination(id, date);
                if (isDeleted)
                {
                    return Ok("Vaccination deleted successfully.");
                }
                else
                {
                    return NotFound($"Vaccination record with id = {id} and date = {date} not found.");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("vaccination/all")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Vaccination>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetAllVaccinations()
        {
            try
            {
                List<Vaccination> vaccinations = DBServices.GetAllVaccinations();
                return Ok(vaccinations);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        #endregion
        #region Growth Actions

        [HttpPost("growth/add")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Growth))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddGrowth([FromBody] Growth growth)
        {
            try
            {
                if (DBServices.AddGrowth(growth))
                {
                    return CreatedAtAction(nameof(GetGrowthById), new { id = growth.idChild, date = growth.timeDate }, growth);
                }
                return BadRequest("Failed to add the growth record.");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("growth/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Growth))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetGrowthById(string id, [FromQuery] DateTime date, int typeGrowth)
        {
            try
            {
                var growth = DBServices.GetGrowthById(id, date, typeGrowth);
                if (growth != null)
                {
                    return Ok(growth);
                }
                return NotFound($"Growth record with id = {id} and date = {date} not found.");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("growth/edit")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult EditGrowth([FromBody] Growth growth)
        {
            try
            {
                if (DBServices.EditGrowth(growth))
                {
                    return Ok("Growth record updated successfully.");
                }
                return BadRequest("Failed to update the growth record.");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("growth/delete/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteGrowth(string id, [FromQuery] DateTime date, int typeGrowth)
        {
            try
            {
                if (DBServices.DeleteGrowth(id, date, typeGrowth))
                {
                    return Ok("Growth record deleted successfully.");
                }
                return NotFound($"Growth record with id = {id} and date = {date} not found.");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("growth/all")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Growth>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<List<Growth>> GetAllGrowths()
        {
            try
            {
                var growths = DBServices.GetAllGrowths();
                return Ok(growths);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        #endregion
        [HttpPost("TypeVaccination/add")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(TypeVaccination))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddTypeVaccination([FromBody] TypeVaccination typeVaccination)
        {
            try
            {
                bool isAdded = DBServices.AddTypeVaccination(typeVaccination);
                if (isAdded)
                {
                    return CreatedAtAction(nameof(GetTypeVaccinationById), new { id = typeVaccination.TypeVaccinationId }, typeVaccination);
                }
                else
                {
                    return BadRequest("Failed to add the vaccination type.");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("TypeVaccination/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TypeVaccination))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetTypeVaccinationById(int id)
        {
            try
            {
                TypeVaccination typeVaccination = DBServices.GetTypeVaccinationById(id);
                if (typeVaccination != null)
                {
                    return Ok(typeVaccination);
                }
                else
                {
                    return NotFound($"Vaccination type with ID = {id} not found.");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("TypeVaccination/edit")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult EditTypeVaccination([FromBody] TypeVaccination typeVaccination)
        {
            try
            {
                bool isEdited = DBServices.EditTypeVaccination(typeVaccination);
                if (isEdited)
                {
                    return Ok("Vaccination type updated successfully.");
                }
                else
                {
                    return BadRequest("Failed to update the vaccination type.");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("TypeVaccination/delete/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteTypeVaccination(int id)
        {
            try
            {
                bool isDeleted = DBServices.DeleteTypeVaccination(id);
                if (isDeleted)
                {
                    return Ok("Vaccination type deleted successfully.");
                }
                else
                {
                    return NotFound($"Vaccination type with ID = {id} not found.");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("TypeVaccination/all")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<TypeVaccination>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetAllTypeVaccinations()
        {
            try
            {
                List<TypeVaccination> types = DBServices.GetAllTypeVaccinations();
                return Ok(types);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
