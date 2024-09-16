using A_AAsesoresWeb.Entities;
using A_AAsesoresWeb.Models;
using System;
using System.Web.Mvc;
using System.Linq;
using System.Collections.Generic;


namespace A_AAsesoresWeb.Controllers
{
    public class MaintenanceController : AlertsController
    {
        readonly MaintenanceModel maintenanceModel = new MaintenanceModel();
        PropertyAddressModel propertyAddressModel = new PropertyAddressModel();

        [HttpGet]
        public ActionResult MaintenanceList()
        {
            MaintenanceEnt maintenanceEnt = new MaintenanceEnt();
            maintenanceEnt.Roles = maintenanceModel.GetAllRole();
            maintenanceEnt.TransactionType = maintenanceModel.GetAllTransactionTypes();
            maintenanceEnt.DocumentType = maintenanceModel.GetAllDocumentTypes();
            maintenanceEnt.PropertyType = maintenanceModel.GetAllPropertyTypes();
            maintenanceEnt.Status = maintenanceModel.GetAllStatuses();
            maintenanceEnt.Province = maintenanceModel.GetProvinces();
            maintenanceEnt.Province = maintenanceModel.GetProvinces();
            maintenanceEnt.District = maintenanceModel.GetDistrictsSP();
            maintenanceEnt.Canton = maintenanceModel.GetCantons();
            maintenanceEnt.Room = maintenanceModel.GetQuoteRooms();

            return View(maintenanceEnt);
        }

        [HttpGet]
        public ActionResult AddRole()
        {
            var isActiveOptions = new List<SelectListItem>
        {
            new SelectListItem { Value = "true", Text = "Activo" },
            new SelectListItem { Value = "false", Text = "Inactivo" }
        };

            ViewBag.IsActiveOptions = isActiveOptions;
            TempData.Clear();
            return View();
        }


        readonly RoleModel roleModel = new RoleModel();

        [HttpPost]
        public ActionResult AddRole(RoleEnt entidad)
        {
            if (ModelState.IsValid && !string.IsNullOrWhiteSpace(entidad.RoleName) && !string.IsNullOrWhiteSpace(entidad.Description))
            {
                try
                {
                    int result = roleModel.RegisterRole(entidad);

                    if (result >= 1)
                    {
                        TempData["notification"] = "Se ha agregado con éxito.";
                        TempData["notificationType"] = "success";
                        return Json(new { success = true, message = TempData["notification"] });
                    }
                    else
                    {
                        TempData["notification"] = "Error al registrar.";
                        TempData["notificationType"] = "error";
                    }
                }
                catch (Exception ex)
                {
                    TempData["notification"] = "Error al registrar. " + ex.Message;
                    TempData["notificationType"] = "error";
                }
            }
            else
            {
                TempData["notification"] = "Error: Por favor, complete todos los campos requeridos.";
                TempData["notificationType"] = "error";
            }

            return Json(new { success = false, message = TempData["notification"] });
        }

        [HttpGet]
        public ActionResult AddStatus()
        {
            var isActiveOptions = new List<SelectListItem>
        {
            new SelectListItem { Value = "true", Text = "Activo" },
            new SelectListItem { Value = "false", Text = "Inactivo" }
        };

            var isTable = new List<SelectListItem>
        {
            new SelectListItem { Value = "Empleado", Text = "Empleado" },
            new SelectListItem { Value = "Propiedades", Text = "Propiedades" },
            new SelectListItem { Value = "Citas", Text = "Citas" },
            new SelectListItem { Value = "Cotizaciones", Text = "Cotizaciones" },
            new SelectListItem { Value = "Contacto", Text = "Contacto" },

        };
            ViewBag.isTable = isTable;
            ViewBag.IsActiveOptions = isActiveOptions;
            TempData.Clear();
            return View();
        }


        readonly StatusModel statusModel = new StatusModel();

        [HttpPost]
        public ActionResult AddStatus(StatusEnt entidad)
        {
            if (ModelState.IsValid && !string.IsNullOrWhiteSpace(entidad.Name) && !string.IsNullOrWhiteSpace(entidad.Description))
            {
                try
                {
                    int result = statusModel.RegisterStatus(entidad);

                    if (result >= 1)
                    {
                        TempData["notification"] = "Se ha agregado con éxito.";
                        TempData["notificationType"] = "success";
                        return Json(new { success = true, message = TempData["notification"] });
                    }
                    else
                    {
                        TempData["notification"] = "Error al registrar.";
                        TempData["notificationType"] = "error";
                    }
                }
                catch (Exception ex)
                {
                    TempData["notification"] = "Error al registrar. " + ex.Message;
                    TempData["notificationType"] = "error";
                }
            }
            else
            {
                TempData["notification"] = "Error: Por favor, complete todos los campos requeridos.";
                TempData["notificationType"] = "error";
            }

            return Json(new { success = false, message = TempData["notification"] });
        }

        [HttpGet]
        public ActionResult AddPropertyType()
        {
            var isActiveOptions = new List<SelectListItem>
        {
            new SelectListItem { Value = "true", Text = "Activo" },
            new SelectListItem { Value = "false", Text = "Inactivo" }
        };

            ViewBag.IsActiveOptions = isActiveOptions;
            TempData.Clear();
            return View();
        }


        readonly PropertyTypeModel propertyTypeModel = new PropertyTypeModel();

        [HttpPost]
        public ActionResult AddPropertyType(PropertyTypeEnt entidad)
        {
            if (ModelState.IsValid && !string.IsNullOrWhiteSpace(entidad.Type) && !string.IsNullOrWhiteSpace(entidad.Description))
            {
                try
                {
                    int result = propertyTypeModel.RegisterPropertyType(entidad);

                    if (result >= 1)
                    {
                        TempData["notification"] = "Se ha agregado con éxito.";
                        TempData["notificationType"] = "success";
                        return Json(new { success = true, message = TempData["notification"] });
                    }
                    else
                    {
                        TempData["notification"] = "Error al registrar.";
                        TempData["notificationType"] = "error";
                    }
                }
                catch (Exception ex)
                {
                    TempData["notification"] = "Error al registrar. " + ex.Message;
                    TempData["notificationType"] = "error";
                }
            }
            else
            {
                TempData["notification"] = "Error: Por favor, complete todos los campos requeridos.";
                TempData["notificationType"] = "error";
            }

            return Json(new { success = false, message = TempData["notification"] });
        }

        [HttpGet]
        public ActionResult AddTransactionType()
        {
            var isActiveOptions = new List<SelectListItem>
            {
                new SelectListItem { Value = "true", Text = "Activo" },
                new SelectListItem { Value = "false", Text = "Inactivo" }
            };

            ViewBag.IsActiveOptions = isActiveOptions;
            TempData.Clear();
            return View();
        }

        readonly TransactionTypeModel transactionTypeModel = new TransactionTypeModel();

        [HttpPost]
        public ActionResult AddTransactionType(TransactionTypeEnt entidad)
        {
            if (ModelState.IsValid && !string.IsNullOrWhiteSpace(entidad.Type) && !string.IsNullOrWhiteSpace(entidad.Description))
            {
                try
                {
                    int result = transactionTypeModel.RegisterTransactionType(entidad);

                    if (result >= 1)
                    {
                        TempData["notification"] = "Se ha agregado con éxito.";
                        TempData["notificationType"] = "success";
                        return Json(new { success = true, message = TempData["notification"] });
                    }
                    else
                    {
                        TempData["notification"] = "Error al registrar.";
                        TempData["notificationType"] = "error";
                    }
                }
                catch (Exception ex)
                {
                    TempData["notification"] = "Error al registrar. " + ex.Message;
                    TempData["notificationType"] = "error";
                }
            }
            else
            {
                TempData["notification"] = "Error: Por favor, complete todos los campos requeridos.";
                TempData["notificationType"] = "error";
            }

            return Json(new { success = false, message = TempData["notification"] });
        }


        [HttpGet]
        public ActionResult AddDocumentType()
        {
            var isActiveOptions = new List<SelectListItem>
        {
            new SelectListItem { Value = "true", Text = "Activo" },
            new SelectListItem { Value = "false", Text = "Inactivo" }
        };

            ViewBag.IsActiveOptions = isActiveOptions;
            TempData.Clear();
            return View();
        }


        readonly DocumentTypeModel documentTypeModel = new DocumentTypeModel();

        [HttpPost]
        public ActionResult AddDocumentType(DocumentTypeEnt entidad)
        {
            if (ModelState.IsValid && !string.IsNullOrWhiteSpace(entidad.Document))
            {
                try
                {
                    int result = documentTypeModel.RegisterDocumentType(entidad);

                    if (result >= 1)
                    {
                        TempData["notification"] = "Se ha agregado con éxito.";
                        TempData["notificationType"] = "success";
                        return Json(new { success = true, message = TempData["notification"] });
                    }
                    else
                    {
                        TempData["notification"] = "Error al registrar.";
                        TempData["notificationType"] = "error";
                    }
                }
                catch (Exception ex)
                {
                    TempData["notification"] = "Error al registrar el Estado. " + ex.Message;
                    TempData["notificationType"] = "error";
                }
            }
            else
            {
                TempData["notification"] = "Error: Por favor, complete todos los campos requeridos.";
                TempData["notificationType"] = "error";
            }

            return Json(new { success = false, message = TempData["notification"] });
        }

        [HttpGet]
        public ActionResult AddProvince()
        {
            var isActiveOptions = new List<SelectListItem>
        {
            new SelectListItem { Value = "true", Text = "Activo" },
            new SelectListItem { Value = "false", Text = "Inactivo" }
        };

            ViewBag.IsActiveOptions = isActiveOptions;
            TempData.Clear();
            return View();
        }


        readonly MaintenanceModel provinceModel = new MaintenanceModel();

        [HttpPost]
        public ActionResult AddProvince(ProvinceEnt entidad)
        {
            if (ModelState.IsValid && !string.IsNullOrWhiteSpace(entidad.Name))
            {
                try
                {
                    int result = provinceModel.RegisterProvince(entidad);

                    if (result >= 1)
                    {
                        TempData["notification"] = "Se ha agregado con éxito.";
                        TempData["notificationType"] = "success";
                        return Json(new { success = true, message = TempData["notification"] });
                    }
                    else
                    {
                        TempData["notification"] = "Error al registrar.";
                        TempData["notificationType"] = "error";
                        return Json(new { success = false, message = TempData["notification"] });
                    }
                }
                catch (Exception ex)
                {
                    TempData["notification"] = "Error al registrar. " + ex.Message;
                    TempData["notificationType"] = "error";
                    return Json(new { success = false, message = TempData["notification"] });
                }
            }
            else
            {
                TempData["notification"] = "Error: Por favor, complete todos los campos requeridos.";
                TempData["notificationType"] = "error";
                return Json(new { success = false, message = TempData["notification"] });
            }

        }

        [HttpGet]
        public ActionResult AddCanton()
        {
            var isActiveOptions = new List<SelectListItem>
        {
            new SelectListItem { Value = "true", Text = "Activo" },
            new SelectListItem { Value = "false", Text = "Inactivo" }
        };
            ViewBag.Provinces = propertyAddressModel.ListProvinces();
            ViewBag.Cantones = propertyAddressModel.ListCantons(1);
            ViewBag.Districts = propertyAddressModel.ListDistricts(1);
            ViewBag.IsActiveOptions = isActiveOptions;
            TempData.Clear();
            return View();
        }


        readonly MaintenanceModel cantonModel = new MaintenanceModel();

        [HttpPost]
        public ActionResult AddCanton(CantonEnt entidad)
        {
            if (ModelState.IsValid && !string.IsNullOrWhiteSpace(entidad.Name) && entidad.Province != 0)
            {
                try
                {
                    int result = cantonModel.RegisterCanton(entidad);

                    if (result >= 1)
                    {
                        TempData["notification"] = "Se ha agregado con éxito.";
                        TempData["notificationType"] = "success";
                        ViewBag.Provinces = propertyAddressModel.ListProvinces();
                        ViewBag.Cantones = propertyAddressModel.ListCantons(1);
                        ViewBag.Districts = propertyAddressModel.ListDistricts(1);
                        return Json(new { success = true, message = TempData["notification"] });
                    }
                    else
                    {
                        TempData["notification"] = "Error al registrar.";
                        TempData["notificationType"] = "error";
                        ViewBag.Provinces = propertyAddressModel.ListProvinces();
                        ViewBag.Cantones = propertyAddressModel.ListCantons(1);
                        ViewBag.Districts = propertyAddressModel.ListDistricts(1);
                        return Json(new { success = false, message = TempData["notification"] });
                    }
                }
                catch (Exception ex)
                {
                    TempData["notification"] = "Error al registrar. " + ex.Message;
                    TempData["notificationType"] = "error";
                    ViewBag.Provinces = propertyAddressModel.ListProvinces();
                    ViewBag.Cantones = propertyAddressModel.ListCantons(1);
                    ViewBag.Districts = propertyAddressModel.ListDistricts(1);
                    return Json(new { success = false, message = TempData["notification"] });
                }
            }
            else
            {
                TempData["notification"] = "Error: Por favor, complete todos los campos requeridos.";
                TempData["notificationType"] = "error";
            }

            return Json(new { success = false, message = TempData["notification"] });
        }

        [HttpGet]
        public ActionResult AddDistrict()
        {
            var isActiveOptions = new List<SelectListItem>
        {
            new SelectListItem { Value = "true", Text = "Activo" },
            new SelectListItem { Value = "false", Text = "Inactivo" }
        };
            ViewBag.Provinces = propertyAddressModel.ListProvinces();
            ViewBag.Cantones = propertyAddressModel.ListCantons(1);
            ViewBag.Districts = propertyAddressModel.ListDistricts(1);
            ViewBag.IsActiveOptions = isActiveOptions;
            TempData.Clear();
            return View();
        }


        readonly MaintenanceModel districtModel = new MaintenanceModel();

        [HttpPost]
        public ActionResult AddDistrict(DistrictEnt entidad)
        {
            if (ModelState.IsValid && !string.IsNullOrWhiteSpace(entidad.Name) && entidad.Canton != 0)
            {
                try
                {
                    int result = districtModel.RegisterDistrict(entidad);

                    if (result >= 1)
                    {
                        TempData["notification"] = "Se ha agregado con éxito.";
                        TempData["notificationType"] = "success";
                        ViewBag.Provinces = propertyAddressModel.ListProvinces();
                        ViewBag.Cantones = propertyAddressModel.ListCantons(1);
                        ViewBag.Districts = propertyAddressModel.ListDistricts(1);
                        return Json(new { success = true, message = TempData["notification"] });
                    }
                    else
                    {
                        TempData["notification"] = "Error al registrar.";
                        TempData["notificationType"] = "error";
                        ViewBag.Provinces = propertyAddressModel.ListProvinces();
                        ViewBag.Cantones = propertyAddressModel.ListCantons(1);
                        ViewBag.Districts = propertyAddressModel.ListDistricts(1);
                        return Json(new { success = false, message = TempData["notification"] });
                    }
                }
                catch (Exception ex)
                {
                    TempData["notification"] = "Error al registrar. " + ex.Message;
                    TempData["notificationType"] = "error";
                    ViewBag.Provinces = propertyAddressModel.ListProvinces();
                    ViewBag.Cantones = propertyAddressModel.ListCantons(1);
                    ViewBag.Districts = propertyAddressModel.ListDistricts(1);
                    return Json(new { success = false, message = TempData["notification"] });
                }
            }
            else
            {
                TempData["notification"] = "Error: Por favor, complete todos los campos requeridos.";
                TempData["notificationType"] = "error";
                return Json(new { success = false, message = TempData["notification"] });
            }

        }

        [HttpGet]
        public ActionResult ChangeProvinceStatus(int q)
        {
            var entidad = new ProvinceEnt();
            entidad.Id = q;
            var resp = maintenanceModel.ChangeProvinceStatus(entidad);
            if (resp >= 1)
            {
                return RedirectToAction("MaintenanceList");
            }
            else
            {
                ViewBag.MensajeUsuario = "No se pudo actualizar el estado de la provincia";
                return View();
            }
        }

        [HttpGet]
        public ActionResult ChangeCantonStatus(int q)
        {
            var entidad = new CantonEnt();
            entidad.Id = q;
            var resp = maintenanceModel.ChangeCantonStatus(entidad);
            if (resp >= 1)
            {
                return RedirectToAction("MaintenanceList");
            }
            else
            {
                ViewBag.MensajeUsuario = "No se pudo actualizar el estado del canton";
                return View();
            }
        }

        [HttpGet]
        public ActionResult ChangeDistrictStatus(int q)
        {
            var entidad = new PropertyAddressEnt();
            entidad.Id = q;
            var resp = maintenanceModel.ChangeDistrictStatus(entidad);
            if (resp >= 1)
            {
                return RedirectToAction("MaintenanceList");
            }
            else
            {
                ViewBag.MensajeUsuario = "No se pudo actualizar el estado del distrito";
                return View();
            }
        }

        [HttpGet]
        public ActionResult ChangeDocumentTypeStatus(int q)
        {
            var entidad = new DocumentTypeEnt();
            entidad.ID = q;
            var resp = maintenanceModel.ChangeDocumentTypeStatus(entidad);
            if (resp >= 1)
            {
                return RedirectToAction("MaintenanceList");
            }
            else
            {
                ViewBag.MensajeUsuario = "No se pudo actualizar el estado del documento";
                return View();
            }
        }

        [HttpGet]
        public ActionResult ChangeStatusStatus(int q)
        {
            var entidad = new StatusEnt();
            entidad.ID = q;
            var resp = maintenanceModel.ChangeStatusStatus(entidad);
            if (resp >= 1)
            {
                return RedirectToAction("MaintenanceList");
            }
            else
            {
                ViewBag.MensajeUsuario = "No se pudo actualizar el estado del estado";
                return View();
            }
        }
        [HttpGet]
        public ActionResult ChangeRoleStatus(int q)
        {
            var entidad = new RoleEnt();
            entidad.Id = q;
            var resp = maintenanceModel.ChangeRoleStatus(entidad);
            if (resp >= 1)
            {
                return RedirectToAction("MaintenanceList");
            }
            else
            {
                ViewBag.MensajeUsuario = "No se pudo actualizar el estado del role";
                return View();
            }
        }

        [HttpGet]
        public ActionResult ChangePropertyTypeStatus(int q)
        {
            var entidad = new PropertyTypeEnt();
            entidad.Id = q;
            var resp = maintenanceModel.ChangePropertyTypeStatus(entidad);
            if (resp >= 1)
            {
                return RedirectToAction("MaintenanceList");
            }
            else
            {
                ViewBag.MensajeUsuario = "No se pudo actualizar el estado del role";
                return View();
            }
        }

        [HttpGet]
        public ActionResult ChangeTransactionTypesStatus(int q)
        {
            var entidad = new TransactionTypeEnt();
            entidad.Id = q;
            var resp = maintenanceModel.ChangeTransactionTypesStatus(entidad);
            if (resp >= 1)
            {
                return RedirectToAction("MaintenanceList");
            }
            else
            {
                ViewBag.MensajeUsuario = "No se pudo actualizar el estado del role";
                return View();
            }
        }

        [HttpGet]
        public ActionResult UpdateRole(int q)
        {
            var data = maintenanceModel.GetRoleByIdSP(q);


            var isActiveOptions = new List<SelectListItem>
            {
                new SelectListItem { Value = "true", Text = "Activo" },
                new SelectListItem { Value = "false", Text = "Inactivo" }
            };

            ViewBag.IsActiveOptions = isActiveOptions;


            TempData.Clear();

            return View(data);
        }

        [HttpPost]
        public ActionResult UpdateRole(RoleEnt entidad)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    maintenanceModel.UpdateRoleSP(entidad);
                    TempData["notification"] = "Se ha actualizado con éxito.";
                    TempData["notificationType"] = "success";
                    return Json(new { success = true, message = TempData["notification"] });
                }
                catch (Exception ex)
                {
                    TempData["notification"] = "Error al actualizar el rol: " + ex.Message;
                    TempData["notificationType"] = "error";
                    return Json(new { success = false, message = TempData["notification"] });
                }
            }
            else
            {
                TempData["notification"] = "Error: Por favor, complete todos los campos requeridos.";
                TempData["notificationType"] = "error";
                return Json(new { success = false, message = TempData["notification"] });
            }
        }

        [HttpGet]
        public ActionResult UpdateStatus(int q)
        {
            var data = maintenanceModel.GetStatusByIdSP(q);


            var isActiveOptions = new List<SelectListItem>
            {
                new SelectListItem { Value = "true", Text = "Activo" },
                new SelectListItem { Value = "false", Text = "Inactivo" }
            };

            ViewBag.IsActiveOptions = isActiveOptions;


            TempData.Clear();

            return View(data);
        }

        [HttpPost]
        public ActionResult UpdateStatus(StatusEnt entidad)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    maintenanceModel.UpdateStatus(entidad);
                    TempData["notification"] = "Se ha actualizado con éxito.";
                    TempData["notificationType"] = "success";
                    return Json(new { success = true, message = TempData["notification"] });
                }
                catch (Exception ex)
                {
                    TempData["notification"] = "Error al actualizar el estado: " + ex.Message;
                    TempData["notificationType"] = "error";
                    return Json(new { success = false, message = TempData["notification"] });
                }
            }
            else
            {
                TempData["notification"] = "Error: Por favor, complete todos los campos requeridos.";
                TempData["notificationType"] = "error";
                return Json(new { success = false, message = TempData["notification"] });
            }
        }

        [HttpGet]
        public ActionResult UpdateTransactionType(int q)
        {
            var data = maintenanceModel.GetTransactionTypesByIdSP(q);


            var isActiveOptions = new List<SelectListItem>
            {
                new SelectListItem { Value = "true", Text = "Activo" },
                new SelectListItem { Value = "false", Text = "Inactivo" }
            };

            ViewBag.IsActiveOptions = isActiveOptions;


            TempData.Clear();

            return View(data);
        }

        [HttpPost]
        public ActionResult UpdateTransactionType(TransactionTypeEnt entidad)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    maintenanceModel.UpdateTransactionType(entidad);
                    TempData["notification"] = "Se ha actualizado con éxito.";
                    TempData["notificationType"] = "success";
                    return Json(new { success = true, message = TempData["notification"] });
                }
                catch (Exception ex)
                {
                    TempData["notification"] = "Error al actualizar el tipo de transaccion: " + ex.Message;
                    TempData["notificationType"] = "error";
                    return Json(new { success = false, message = TempData["notification"] });
                }
            }
            else
            {
                TempData["notification"] = "Error: Por favor, complete todos los campos requeridos.";
                TempData["notificationType"] = "error";
                return Json(new { success = false, message = TempData["notification"] });
            }
        }

        [HttpGet]
        public ActionResult UpdatePropertyType(int q)
        {
            var data = maintenanceModel.GetPropertyTypesByIdSP(q);


            var isActiveOptions = new List<SelectListItem>
            {
                new SelectListItem { Value = "true", Text = "Activo" },
                new SelectListItem { Value = "false", Text = "Inactivo" }
            };

            ViewBag.IsActiveOptions = isActiveOptions;


            TempData.Clear();

            return View(data);
        }

        [HttpPost]
        public ActionResult UpdatePropertyType(PropertyTypeEnt entidad)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    maintenanceModel.UpdatePropertyType(entidad);
                    TempData["notification"] = "Se ha actualizado con éxito.";
                    TempData["notificationType"] = "success";
                    return Json(new { success = true, message = TempData["notification"] });
                }
                catch (Exception ex)
                {
                    TempData["notification"] = "Error al actualizar el tipo de propiedad: " + ex.Message;
                    TempData["notificationType"] = "error";
                    return Json(new { success = false, message = TempData["notification"] });
                }
            }
            else
            {
                TempData["notification"] = "Error: Por favor, complete todos los campos requeridos.";
                TempData["notificationType"] = "error";
                return Json(new { success = false, message = TempData["notification"] });
            }
        }

        [HttpGet]
        public ActionResult UpdateDocumentType(int q)
        {
            var data = maintenanceModel.GetDocumentTypeByIdSP(q);


            var isActiveOptions = new List<SelectListItem>
            {
                new SelectListItem { Value = "true", Text = "Activo" },
                new SelectListItem { Value = "false", Text = "Inactivo" }
            };

            ViewBag.IsActiveOptions = isActiveOptions;


            TempData.Clear();

            return View(data);
        }

        [HttpPost]
        public ActionResult UpdateDocumentType(DocumentTypeEnt entidad)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    maintenanceModel.UpdateDocumentType(entidad);
                    TempData["notification"] = "Se ha actualizado con éxito.";
                    TempData["notificationType"] = "success";
                    return Json(new { success = true, message = TempData["notification"] });
                }
                catch (Exception ex)
                {
                    TempData["notification"] = "Error al actualizar la provincia: " + ex.Message;
                    TempData["notificationType"] = "error";
                    return Json(new { success = false, message = TempData["notification"] });
                }
            }
            else
            {
                TempData["notification"] = "Error: Por favor, complete todos los campos requeridos.";
                TempData["notificationType"] = "error";
                return Json(new { success = false, message = TempData["notification"] });
            }
        }


        [HttpGet]
        public ActionResult UpdateProvince(int q)
        {
            var data = maintenanceModel.GetProvinceById(q);


            var isActiveOptions = new List<SelectListItem>
            {
                new SelectListItem { Value = "true", Text = "Activo" },
                new SelectListItem { Value = "false", Text = "Inactivo" }
            };

            ViewBag.IsActiveOptions = isActiveOptions;


            TempData.Clear();

            return View(data);
        }

        [HttpPost]
        public ActionResult UpdateProvince(ProvinceEnt entidad)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    maintenanceModel.UpdateProvince(entidad);
                    TempData["notification"] = "Se ha actualizado con éxito.";
                    TempData["notificationType"] = "success";
                    return Json(new { success = true, message = TempData["notification"] });
                }
                catch (Exception ex)
                {
                    TempData["notification"] = "Error al actualizar la provincia: " + ex.Message;
                    TempData["notificationType"] = "error";
                    return Json(new { success = false, message = TempData["notification"] });
                }
            }
            else
            {
                TempData["notification"] = "Error: Por favor, complete todos los campos requeridos.";
                TempData["notificationType"] = "error";
                return Json(new { success = false, message = TempData["notification"] });
            }
        }

        [HttpGet]
        public ActionResult EditCantonSP(int q)
        {
            var data = maintenanceModel.GetCantonById(q);


            var isActiveOptions = new List<SelectListItem>
            {
                new SelectListItem { Value = "true", Text = "Activo" },
                new SelectListItem { Value = "false", Text = "Inactivo" }
            };

            ViewBag.IsActiveOptions = isActiveOptions;


            TempData.Clear();

            return View(data);
        }

        [HttpPost]
        public ActionResult EditCantonSP(CantonEnt entidad)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    maintenanceModel.EditCantonSP(entidad);
                    TempData["notification"] = "Se ha actualizado con éxito.";
                    TempData["notificationType"] = "success";
                    return Json(new { success = true, message = TempData["notification"] });
                }
                catch (Exception ex)
                {
                    TempData["notification"] = "Error al actualizar rl canton: " + ex.Message;
                    TempData["notificationType"] = "error";
                    return Json(new { success = false, message = TempData["notification"] });
                }
            }
            else
            {
                TempData["notification"] = "Error: Por favor, complete todos los campos requeridos.";
                TempData["notificationType"] = "error";
                return Json(new { success = false, message = TempData["notification"] });
            }
        }

        [HttpGet]
        public ActionResult EditDistrictSP(int q)
        {
            var data = maintenanceModel.GetDistrictByIdSP(q);


            var isActiveOptions = new List<SelectListItem>
            {
                new SelectListItem { Value = "true", Text = "Activo" },
                new SelectListItem { Value = "false", Text = "Inactivo" }
            };

            ViewBag.IsActiveOptions = isActiveOptions;


            TempData.Clear();

            return View(data);
        }


        [HttpPost]
        public ActionResult EditDistrictSP(DistrictEnt entidad)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    maintenanceModel.EditDistrictSP(entidad);
                    TempData["notification"] = "Se ha actualizado con éxito.";
                    TempData["notificationType"] = "success";
                    return Json(new { success = true, message = TempData["notification"] });
                }
                catch (Exception ex)
                {
                    TempData["notification"] = "Error al actualizar el distrito: " + ex.Message;
                    TempData["notificationType"] = "error";
                    return Json(new { success = false, message = TempData["notification"] });
                }
            }
            else
            {
                TempData["notification"] = "Error: Por favor, complete todos los campos requeridos.";
                TempData["notificationType"] = "error";
                return Json(new { success = false, message = TempData["notification"] });
            }
        }

        //Método de habitaciones
        readonly QuoteRoomModel quoteRoomModel = new QuoteRoomModel();

        [HttpGet]
        public ActionResult AddRoom()
        {
            var isActiveOptions = new List<SelectListItem>
        {
            new SelectListItem { Value = "true", Text = "Activo" },
            new SelectListItem { Value = "false", Text = "Inactivo" }
        };

            ViewBag.IsActiveOptions = isActiveOptions;
            TempData.Clear();
            return View();
        }

        [HttpPost]
        public ActionResult AddRoom(QuoteRoomEnt entidad)
        {
            if (ModelState.IsValid && !string.IsNullOrWhiteSpace(entidad.RoomName))
            {
                try
                {
                    if (quoteRoomModel.RegisterRoom(entidad) >= 1)
                    {
                        TempData["notification"] = "Se ha agregado con éxito.";
                        TempData["notificationType"] = "success";
                        return Json(new { success = true, message = TempData["notification"] });
                    }
                    else
                    {
                        TempData["notification"] = "Error al registrar.";
                        TempData["notificationType"] = "error";
                    }
                }
                catch (Exception ex)
                {
                    TempData["notification"] = "Error al registrar. " + ex.Message;
                    TempData["notificationType"] = "error";
                }
            }
            else
            {
                TempData["notification"] = "Error: Por favor, complete todos los campos requeridos.";
                TempData["notificationType"] = "error";
            }

            return Json(new { success = false, message = TempData["notification"] });
        }

        //Obtener una habitación por ID para actualizar.
        [HttpGet]
        public ActionResult EditRoom(int q)
        {
            var data = maintenanceModel.GetRoomById(q);
            var isActiveOptions = new List<SelectListItem>
            {
                new SelectListItem { Value = "true", Text = "Activo" },
                new SelectListItem { Value = "false", Text = "Inactivo" }
            };
            ViewBag.IsActiveOptions = isActiveOptions;
            TempData.Clear();
            return View(data);
        }

        [HttpPost]
        public ActionResult EditRoom(QuoteRoomEnt entidad)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (quoteRoomModel.UpdateQuoteRoom(entidad) >= 1)
                    {

                        TempData["notification"] = "Se ha actualizado con éxito.";
                        TempData["notificationType"] = "success";
                    }
                    else
                    {
                        TempData["notification"] = "Error al actualizar.";
                        TempData["notificationType"] = "error";
                    }
                    return Json(new { success = true, message = TempData["notification"] });
                }
                catch (Exception ex)
                {
                    TempData["notification"] = "Error al actualizar la habitación: " + ex.Message;
                    TempData["notificationType"] = "error";
                    return Json(new { success = false, message = TempData["notification"] });
                }
            }
            else
            {
                TempData["notification"] = "Error: Por favor, complete todos los campos requeridos.";
                TempData["notificationType"] = "error";
                return Json(new { success = false, message = TempData["notification"] });
            }
        }


        //Actualiza el estado de una Habitación.
        [HttpGet]
        public ActionResult ChangeRoomStatus(int q)
        {
            var entidad = new QuoteRoomEnt();
            entidad.Id = q;
            var resp = maintenanceModel.ChangeRoomStatus(entidad);
            if (resp >= 1)
            {
                return RedirectToAction("MaintenanceList");
            }
            else
            {
                ViewBag.MensajeUsuario = "No se pudo actualizar el estado del distrito";
                return View();
            }
        }
    }
}
