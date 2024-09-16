using A_AAsesoresAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace A_AAsesoresAPI.Controllers
{
    [Authorize]
    public class DocumentTypeController : ApiController
    {
        /*Insertar un nuevo Tipo de Documento*/
        [HttpPost]
        [Route("RegisterDocumentType")]
        public int RegisterDocumentType(DocumentTypeEnt entidad)
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    return context.InsertDocumentTypeSP(entidad.Document);
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }

        /*Actualizar Tipo de Documento*/
        [HttpPut]
        [Route("UpdateDocumentType")]
        public int UpdateDocumentType(DocumentTypeEnt entidad)
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    return context.UpdateDocumentTypeSP(entidad.Id, entidad.Document, entidad.IsActive);
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }
        /*Eliminar Tipo de Documento*/
        [HttpDelete]
        [Route("DeleteDocumentType")]
        public int DeleteDocumentType(int q)
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    return context.DeleteDocumentTypeSP(q);
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }

        [AllowAnonymous]
        /*Obtiene los tipos de documento en formato de Dropdown */
        [HttpGet]
        [Route("ListDocumentType")]
        public List<System.Web.Mvc.SelectListItem> ListDocumentType()
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    var datos = (context.GetAllDocumentTypeSP()).ToList();

                    var respuesta = new List<System.Web.Mvc.SelectListItem>();
                    respuesta.Add(new System.Web.Mvc.SelectListItem { Value = "", Text = "Seleccione un tipo de documento" });
                    foreach (var item in datos)
                    {
                        respuesta.Add(new System.Web.Mvc.SelectListItem { Value = item.Id.ToString(), Text = item.Document });
                    }

                    return respuesta;
                }
            }
            catch (Exception)
            {
                return new List<System.Web.Mvc.SelectListItem>();
            }
        }

        [HttpGet]
        [Route("GetAllDocumentType")]
        public List<GetDocumentTypeSP_Result> GetAllDocumentType()
        {
            using (var context = new AAAsesoresEntities())
            {
                return context.GetDocumentTypeSP().ToList();
            }
        }

        [HttpPut]
        [Route("ChangeDocumentTypeStatus")]
        public int ChangeDocumentTypeStatus(DocumentTypeEnt entidad)
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    context.ChangeDocumentTypeStatusSP(entidad.Id);
                    return 1;
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }


        [HttpGet]
        [Route("GetDocumentTypeByIdSP")]
        public GetDocumentTypeByIdSP_Result GetDocumentTypeByIdSP(int q)
        {
            try
            {
                using (var context = new AAAsesoresEntities())
                {
                    return context.GetDocumentTypeByIdSP(q).FirstOrDefault();
                }
            }
            catch (Exception)
            {

                return null;
            }
        }
    }
}

