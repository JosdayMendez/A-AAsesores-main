using A_AAsesoresAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace A_AAsesoresAPI.Controllers
{
    [Authorize]
    public class PropertyAddressController : ApiController
    {
        [HttpPost]
        [Route("RegisterPropertyAddress")]
        public int RegisterPropertyAddress(PropertyAddressEnt entidad)
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    return context.InsertPropertyAddressSP(entidad.PropertyId, entidad.DistrictId, entidad.OtherSigns);
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("GetPropertiesAddresses")]
        public List<GetAllPropertyAddressSP_Result> GetPropertiesAddresses()
        {
            using (var context = new AAAsesoresEntities())
            {
                return context.GetAllPropertyAddressSP().ToList();
            }
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("GetPropertyAddress")]
        public GetPropertyAddressSP_Result GetPropertyAddress(int q)
        {
            using (var context = new AAAsesoresEntities())
            {
                return context.GetPropertyAddressSP(q).FirstOrDefault();
            }
        }

        [HttpPut]
        [Route("UpdatePropertyAddress")]
        public int UpdatePropertyAddress(PropertyAddressEnt entidad)
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    return context.UpdatePropertyAddressSP(entidad.Id, entidad.PropertyId, entidad.DistrictId, entidad.OtherSigns);
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }

        [HttpDelete]
        [Route("DeletePropertyAddress")]
        public int DeletePropertyAddress(int q)
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    return context.DeletePropertyAddressSP(q);
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }

        [HttpGet]
        [Route("ListProvinces")]
        public List<System.Web.Mvc.SelectListItem> ListProvinces()
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    var datos = (context.GetProvincesSP()).ToList();
                    List<System.Web.Mvc.SelectListItem> lista = new List<System.Web.Mvc.SelectListItem>();
                    lista.Add(new System.Web.Mvc.SelectListItem { Value = "", Text = "Por favor seleccione" });
                    foreach (var item in datos)
                    {
                        lista.Add(new System.Web.Mvc.SelectListItem() { Text = item.Name, Value = item.Id.ToString() });
                    }
                    return lista;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        [HttpGet]
        [Route("GetProvinces")]
        public List<GetProvincesSP_Result> GetProvinces()
        {
            using (var context = new AAAsesoresEntities())
            {
                return context.GetProvincesSP().ToList();
            }
        }

        [HttpPut]
        [Route("UpdateProvince")]
        public int EditProvince(ProvinceEnt entidad)
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    return context.EditProvinceSP(entidad.Id, entidad.Name);
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }

        [HttpPut]
        [Route("EditCantonSP")]
        public int EditCantonSP(CantonEnt entidad)
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    return context.EditCantonSP(entidad.Id, entidad.Name, entidad.Province, entidad.IsActive);
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }


        [HttpPut]
        [Route("EditDistrictSP")]
        public int EditDistrictSP(DistrictEnt entidad)
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    return context.EditDistrictSP(entidad.Id, entidad.Name, entidad.Canton, entidad.IsActive);
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("ListCantons")]
        public List<System.Web.Mvc.SelectListItem> ListCantons(int q)
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    var datos = (context.GetAllCantonsSP(q)).ToList();
                    List<System.Web.Mvc.SelectListItem> lista = new List<System.Web.Mvc.SelectListItem>();
                    lista.Add(new System.Web.Mvc.SelectListItem { Value = "", Text = "Por favor seleccione" });
                    foreach (var item in datos)
                    {
                        lista.Add(new System.Web.Mvc.SelectListItem() { Text = item.Name, Value = item.Id.ToString() });
                    }
                    return lista;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("GetCantons")]
        public List<GetCantonsSP_Result> GetCantons()
        {
            using (var context = new AAAsesoresEntities())
            {
                return context.GetCantonsSP().ToList();
            }
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("ListDistricts")]
        public List<System.Web.Mvc.SelectListItem> ListDistricts(int q)
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    var datos = (context.GetAllDistrictsSP(q)).ToList();
                    List<System.Web.Mvc.SelectListItem> lista = new List<System.Web.Mvc.SelectListItem>();
                    lista.Add(new System.Web.Mvc.SelectListItem { Value = "", Text = "Por favor seleccione" });
                    foreach (var item in datos)
                    {
                        lista.Add(new System.Web.Mvc.SelectListItem() { Text = item.Name, Value = item.Id.ToString() });
                    }
                    return lista;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        [HttpGet]
        [Route("GetDistrictsSP")]
        public List<GetDistrictsSP_Result> GetDistrictsSP()
        {
            using (var context = new AAAsesoresEntities())
            {
                return context.GetDistrictsSP().ToList();
            }
        }

        [HttpPut]
        [Route("ChangeProvinceStatus")]
        public int ChangeProvinceStatus(ProvinceEnt entidad)
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    context.ChangeProvinceStatusSP(entidad.Id);
                    return 1;
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }

        [HttpGet]
        [Route("GetProvinceById")]
        public GetProvinceByIdSP_Result GetProvinceById(int q)
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    return context.GetProvinceByIdSP(q).FirstOrDefault();
                }
            }
            catch (Exception)
            {

                return null;
            }
        }

        [HttpPut]
        [Route("ChangeCantonStatus")]
        public int ChangeCantonStatus(CantonEnt entidad)
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    context.ChangeCantonStatusSP(entidad.Id);
                    return 1;
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }

        [HttpGet]
        [Route("GetCantonById")]
        public GetCantonByIdSP_Result GetCantonById(int q)
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    return context.GetCantonByIdSP(q).FirstOrDefault();
                }
            }
            catch (Exception)
            {

                return null;
            }
        }

        [HttpPut]
        [Route("ChangeDistrictStatus")]
        public int ChangeDistrictStatus(DistrictEnt entidad)
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    context.ChangeDistrictStatusSP(entidad.Id);
                    return 1;
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }

        [HttpGet]
        [Route("GetDistrictByIdSP")]
        public GetDistrictByIdSP_Result GetDistrictByIdSP(int q)
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    return context.GetDistrictByIdSP(q).FirstOrDefault();
                }
            }
            catch (Exception)
            {

                return null;
            }
        }

        [HttpPost]
        [Route("RegisterProvince")]
        public int RegisterProvince(ProvinceEnt entidad)
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    return context.InsertProvinceSP(entidad.Name);
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }

        [HttpPost]
        [Route("RegisterCanton")]
        public int RegisterCanton(CantonEnt entidad)
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    return context.InsertCantonSP(entidad.Name, entidad.Province);
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }

        [HttpPost]
        [Route("RegisterDistrict")]
        public int RegisterDistrict(DistrictEnt entidad)
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    return context.InsertDistrictSP(entidad.Name, entidad.Canton);
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }

    }
}
