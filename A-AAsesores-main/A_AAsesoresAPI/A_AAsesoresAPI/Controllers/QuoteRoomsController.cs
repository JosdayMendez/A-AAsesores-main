using A_AAsesoresAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace A_AAsesoresAPI.Controllers
{
    [Authorize]
    public class QuoteRoomsController : ApiController
    {
        [HttpPost]
        [Route("RegisterQuoteRoom")]
        public int RegisterQuoteRoom(QuoteRoomsEnt entidad)
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    return context.InsertQuoteRoomSP(entidad.RoomName);
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }

        [HttpPut]
        [Route("UpdateQuoteRoom")]
        public int UpdateQuoteRoom(QuoteRoomsEnt entidad)
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    return context.UpdateQuoteRoomSP(entidad.Id, entidad.RoomName, entidad.IsActive);
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }

        [HttpDelete]
        [Route("DeleteQuoteRoom")]
        public int DeleteQuoteRoom(int roomId)
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    return context.DeleteQuoteRoomSP(roomId);
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("GetQuoteRooms")]
        public List<GetRoomsSP_Result> GetQuoteRooms()
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    return context.GetRoomsSP().ToList();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        [HttpGet]
        [Route("GetRoomById")]
        public GetRoomByIdSP_Result GetRoomById(int q)
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    return context.GetRoomByIdSP(q).FirstOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        [HttpPut]
        [Route("ChangeRoomStatus")]
        public int ChangeRoleStatus(RoleEnt entidad)
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    context.ChangeRoomStatusSP(entidad.Id);
                    return 1;
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }
    }
}