using A_AAsesoresWeb.Entities;
using A_AAsesoresWeb.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using static A_AAsesoresWeb.Models.ViewPropertiesModel;

namespace A_AAsesoresWeb.Controllers
{
    public class PropertiesController : Controller
    {
        EmployeeModel employeeModel = new EmployeeModel();
        PropertyModel propertyModel = new PropertyModel();
        TransactionTypeModel transactionTypeModel = new TransactionTypeModel();
        PropertyTypeModel propertyTypeModel = new PropertyTypeModel();
        PropertyAddressModel propertyAddressModel = new PropertyAddressModel();
        PropertyImgModel propertyImgModel = new PropertyImgModel();
        PropertyDocModel propertyDocModel = new PropertyDocModel();
        CurrencyModel currencyModel = new CurrencyModel();

        string ImagesPath = "/AppContent/Properties/Images/Property";
        string DocsPath = "/AppContent/Properties/Docs/Property";

        //Agregar Propiedad
        [SecurityModel]
        [HttpGet]
        public ActionResult AddProperty()
        {
            var employees = employeeModel.ListEmployees();

            if (Session["Role"].ToString().AsInt() == 1)
            {
                ViewBag.Employees = employees;
            }
            else if (Session["Role"].ToString().AsInt() == 2)
            {
                if (employees.Where(x => x.Value.AsInt() == Session["IdTEmployee"].ToString().AsInt()).ToList().Count > 0)
                {
                    ViewBag.Employees = employees.Where(x => x.Value.AsInt() == Session["IdTEmployee"].ToString().AsInt()).ToList();
                }
                else
                {
                    ViewBag.MensajeUsuario = "No tiene citas asignadas en este momento.";
                    return View();
                }
            }

            
            ViewBag.Currencies = currencyModel.ListCurrencies();
            ViewBag.Statuses = propertyModel.ListPropertiesStatuses();
            ViewBag.TransactionTypes = transactionTypeModel.ListTransactionTypes();
            ViewBag.PropertyTypes = propertyTypeModel.ListPropertyTypes();
            ViewBag.Provinces = propertyAddressModel.ListProvinces();
            ViewBag.Cantones = propertyAddressModel.ListCantons(1);
            ViewBag.Districts = propertyAddressModel.ListDistricts(1);
            return View();
        }

        [HttpPost]
        public ActionResult AddProperty(PropertyEnt entidad)
        {
            PropertyEnt tempProperty = new PropertyEnt();
            tempProperty.Title = entidad.Title;
            tempProperty.EmployeeId = entidad.EmployeeId;
            tempProperty.Currency = entidad.Currency;
            tempProperty.Price = entidad.Price;
            tempProperty.AreaLand = entidad.AreaLand;
            tempProperty.AreaBuild = entidad.AreaBuild;
            tempProperty.Description = entidad.Description.Trim();
            tempProperty.PropertyType = entidad.PropertyType;
            tempProperty.TransactionType = entidad.TransactionType;
            tempProperty.PropertyStatus = entidad.PropertyStatus;

            int PropertyId = propertyModel.RegisterProperty(tempProperty);

            if (PropertyId > 0)
            {
                try
                {
                    //Crear, asignar y guardar la dirección de la propiedad
                    PropertyAddressEnt address = new PropertyAddressEnt();
                    address.PropertyId = PropertyId;
                    address.DistrictId = entidad.District;
                    address.OtherSigns = entidad.OtherSigns;
                    propertyAddressModel.RegisterPropertyAddress(address);

                    SaveImages(PropertyId, entidad);
                    SaveDocs(PropertyId, entidad);
                    TempData["notification"] = "La propiedad se registró correctamente.";
                    TempData["notificationType"] = "success";
                    return Json(new { success = true, message = TempData["notification"] + "|" + TempData["notificationType"] });
                    //return RedirectToAction("ListProperties", "Properties");
                }
                catch (System.Exception)
                {
                    ViewBag.MensajeUsuario = "No se pudo guardar la propiedad";
                    ViewBag.Employees = employeeModel.ListEmployees();
                    ViewBag.Currencies = currencyModel.ListCurrencies();
                    ViewBag.Statuses = propertyModel.ListPropertiesStatuses();
                    ViewBag.TransactionTypes = transactionTypeModel.ListTransactionTypes();
                    ViewBag.PropertyTypes = propertyTypeModel.ListPropertyTypes();
                    ViewBag.Provinces = propertyAddressModel.ListProvinces();
                    ViewBag.Cantones = propertyAddressModel.ListCantons(1);
                    ViewBag.Districts = propertyAddressModel.ListDistricts(1);
                    TempData["notification"] = "Se presentó un error al guardar la información de la propiedad.";
                    TempData["notificationType"] = "error";
                    return Json(new { success = false, message = TempData["notification"] + "|" + TempData["notificationType"] });
                    //return View();
                }
            }
            else
            {
                ViewBag.Employees = employeeModel.ListEmployees();
                ViewBag.Currencies = currencyModel.ListCurrencies();
                ViewBag.Statuses = propertyModel.ListPropertiesStatuses();
                ViewBag.TransactionTypes = transactionTypeModel.ListTransactionTypes();
                ViewBag.PropertyTypes = propertyTypeModel.ListPropertyTypes();
                ViewBag.Provinces = propertyAddressModel.ListProvinces();
                ViewBag.Cantones = propertyAddressModel.ListCantons(1);
                ViewBag.Districts = propertyAddressModel.ListDistricts(1);
                TempData["notification"] = "Se produjo un error al intentar registrar la propiedad.";
                TempData["notificationType"] = "error";
                return Json(new { success = false, message = TempData["notification"] + "|" + TempData["notificationType"] });
                //return View();
            }
        }

        //Acutalizar Propiedad
        [SecurityModel]
        [HttpGet]
        public ActionResult UpdateProperty(int q)
        {
            var Property = propertyModel.GetProperty(q);
            var PropertyAddress = propertyAddressModel.GetPropertyAddress(q);

            Property.Province = PropertyAddress.ProvinceId;
            Property.Canton = PropertyAddress.CantonId;
            Property.District = PropertyAddress.DistrictId;
            Property.OtherSigns = PropertyAddress.OtherSigns;

            ViewBag.Employees = employeeModel.ListEmployees();
            ViewBag.Currencies = currencyModel.ListCurrencies();
            ViewBag.Statuses = propertyModel.ListPropertiesStatuses();
            ViewBag.TransactionTypes = transactionTypeModel.ListTransactionTypes();
            ViewBag.PropertyTypes = propertyTypeModel.ListPropertyTypes();
            ViewBag.Provinces = propertyAddressModel.ListProvinces();
            ViewBag.Cantones = propertyAddressModel.ListCantons(Property.Province);
            ViewBag.Districts = propertyAddressModel.ListDistricts(Property.Canton);
            return View(Property);
        }

        [HttpPost]
        public ActionResult UpdateProperty(PropertyEnt entidad)
        {

            propertyModel.UpdateProperty(entidad);
            var Address = propertyAddressModel.GetPropertyAddress(entidad.Id);
            var PropertyAddress = new PropertyAddressEnt();
            PropertyAddress.Id = Address.Id;
            PropertyAddress.PropertyId = Address.PropertyId;
            PropertyAddress.DistrictId = entidad.District;
            PropertyAddress.OtherSigns = entidad.OtherSigns;

            var resp = propertyAddressModel.UpdatePropertyAddress(PropertyAddress);

            if (resp == 1)
            {
                TempData["notification"] = "La propiedad se actualizó correctamente.";
                TempData["notificationType"] = "success";
                return Json(new { success = true, message = TempData["notification"] + "|" + TempData["notificationType"] });
                //return RedirectToAction("ListProperties", "Properties");
            }
            else
            {
                TempData["notification"] = "Se produjo un error al intentar actualizar la propiedad.";
                TempData["notificationType"] = "error";
                return Json(new { success = false, message = TempData["notification"] + "|" + TempData["notificationType"] });
                //return View(entidad.Id);
            }


        }

        [SecurityModel]
        [HttpGet]
        public ActionResult ChangePropertyState(int q)
        {
            PropertyEnt Property = new PropertyEnt();
            Property.Id = q;
            var resp = propertyModel.ChangePropertyState(Property);
            if (resp == 1)
            {
                return RedirectToAction("ListProperties", "Properties");
            }
            else
            {
                ViewBag.MensajeUsuario = "No se pudo actualizar el estado del usuario";
                return View();
            }
        }

        //Vizualizar Propiedad
        [HttpGet]
        public ActionResult ViewProperty(int q)
        {
            PropertyEnt Property = new PropertyEnt();

            var PropertyInfo = propertyModel.GetProperty(q);
            var PropertyAddress = propertyAddressModel.GetPropertyAddress(q);
            var PropertyImages = propertyImgModel.GetPropertyImgs(q);
            //Pendiente
            //var PropertyDocs = propertyDocModel.GetPropertyDocs(q);

            Property.Id = PropertyInfo.Id;
            Property.Title = PropertyInfo.Title;
            Property.EmployeeName = PropertyInfo.EmployeeName;
            Property.EmployeeMail = PropertyInfo.EmployeeMail;
            Property.EmployeePhone = PropertyInfo.EmployeePhone;
            Property.EmployeeImage = PropertyInfo.EmployeeImage;
            Property.Symbol = PropertyInfo.Symbol;
            Property.Currency = PropertyInfo.Currency;
            Property.Price = PropertyInfo.Price;
            Property.AreaLand = PropertyInfo.AreaLand;
            Property.AreaBuild = PropertyInfo.AreaBuild;
            Property.Description = PropertyInfo.Description;
            Property.PropertyTypeName = PropertyInfo.PropertyTypeName;
            Property.TransactionTypeName = PropertyInfo.TransactionTypeName;
            Property.PropertyStatusName = PropertyInfo.PropertyStatusName;
            Property.FullAddress = PropertyAddress.FullDirection;
            Property.OtherSigns = PropertyAddress.OtherSigns;
            Property.ImagesInfo = PropertyImages;
            //Property.Docs = PropertyDocs;

            return View(Property);
        }

        //Vizualizar Properties index que ve el usuario
        [HttpGet]
        public ActionResult ViewProperties(int page = 1, int pageSize = 6)
        {
            ViewPropertiesModel ViewProperties = new ViewPropertiesModel();
            var Properties = new List<PropertyEnt>();

            try
            {
                var PropertyInfo = propertyModel.GetProperties();

                foreach (var Item in PropertyInfo)
                {

                    PropertyEnt Property = new PropertyEnt();

                    var PropertyAddress = propertyAddressModel.GetPropertyAddress(Item.Id);
                    var PropertyImages = propertyImgModel.GetPropertyImgs(Item.Id);

                    Property.Id = Item.Id;
                    Property.Title = Item.Title;
                    Property.EmployeeId = Item.EmployeeId;
                    Property.Symbol = Item.Symbol;
                    Property.Currency = Item.Currency;
                    Property.Price = Item.Price;
                    Property.AreaLand = Item.AreaLand;
                    Property.AreaBuild = Item.AreaBuild;
                    Property.Description = Item.Description;
                    Property.PropertyType = Item.PropertyType;
                    Property.PropertyTypeName = Item.PropertyTypeName;
                    Property.TransactionType = Item.TransactionType;
                    Property.TransactionTypeName = Item.TransactionTypeName;
                    Property.PropertyStatus = Item.PropertyStatus;
                    Property.PropertyStatusName = Item.PropertyStatusName;
                    Property.CreationDate = Item.CreationDate;
                    Property.Province = PropertyAddress.ProvinceId;
                    Property.ProvinceName = PropertyAddress.Province;
                    Property.Canton = PropertyAddress.CantonId;
                    Property.CantonName = PropertyAddress.Canton;
                    Property.District = PropertyAddress.DistrictId;
                    Property.DistrictName = PropertyAddress.District;
                    Property.FullAddress = PropertyAddress.FullDirection;
                    Property.OtherSigns = PropertyAddress.OtherSigns;
                    Property.ImagesInfo = PropertyImages;
                    Properties.Add(Property);
                }

                ViewProperties.PropertyTypeViewModels = Properties
                    .Select(p => new PropertyTypeViewModel { PropertyType = p.PropertyType, PropertyTypeName = p.PropertyTypeName })
                    .GroupBy(p => new { p.PropertyType, p.PropertyTypeName })  // Agrupar por las propiedades que determinan la unicidad
                    .Select(g => g.First())  // Seleccionar el primer elemento de cada grupo
                    .ToList();

                ViewProperties.TransactionTypeViewModels = Properties
                    .Select(p => new TransactionTypeViewModel { TransactionType = p.TransactionType, TransactionTypeName = p.TransactionTypeName })
                    .GroupBy(p => new { p.TransactionType, p.TransactionTypeName })  // Agrupar por las propiedades que determinan la unicidad
                    .Select(g => g.First())  // Seleccionar el primer elemento de cada grupo
                    .ToList();

                ViewProperties.ProvinceViewModels = Properties
                    .Select(p => new ProvinceViewModel { ProvinceId = p.Province, ProvinceName = p.ProvinceName })
                    .GroupBy(p => new { p.ProvinceId, p.ProvinceName })  // Agrupar por las propiedades que determinan la unicidad
                    .Select(g => g.First())  // Seleccionar el primer elemento de cada grupo
                    .ToList();

                var propertiesForPage = Properties.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                ViewBag.TotalPages = (int)Math.Ceiling((double)Properties.Count / pageSize);
                ViewBag.CurrentPage = page;

                if (propertiesForPage.Count() == 0)
                {
                    ViewBag.MensajeUsuario = "No hay propiedades registradas en el sistema.";
                }

                ViewProperties.Properties = propertiesForPage;

                return View(ViewProperties);
            }
            catch (System.Exception)
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult FilterProperties(ViewPropertiesModel filters, int page = 1, int pageSize = 6)
        {
            ViewPropertiesModel ViewProperties = new ViewPropertiesModel();
            try
            {
                //string[] range = filters.Filter.RangePrice.Split(';');
                //filters.Filter.MinPrice = decimal.Parse(range[0]);
                //filters.Filter.MaxPrice = decimal.Parse(range[1]);
                // Filtra tus propiedades según los criterios en filters
                var filteredProperties = FilterPropertiesAccordingToCriteria(filters);

                // Pagina los resultados filtrados
                var propertiesForPage = filteredProperties.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                ViewBag.TotalPages = (int)Math.Ceiling((double)filteredProperties.Count / pageSize);
                ViewBag.CurrentPage = page;
                if (propertiesForPage.Count() == 0)
                {
                    ViewBag.MensajeUsuario = "No se encontraron propiedades que cumplan con los criterios de búsqueda";
                }
                ViewProperties.Properties = propertiesForPage;
                //A partir de este punto está pendiente de revisión
                return PartialView("_PropertyListPartial", ViewProperties);
            }
            catch (System.Exception)
            {
                return PartialView("_ErrorPartial");
            }
        }

        //Vizualizar Propiedad por parte del administrador
        [SecurityModel]
        [HttpGet]
        public ActionResult ListProperties()
        {
            var Properties = new List<PropertyEnt>();

            try
            {
                var PropertyInfo = propertyModel.GetProperties();

                if (PropertyInfo.Count > 0)
                {
                    foreach (var Item in PropertyInfo)
                    {
                        PropertyEnt Property = new PropertyEnt();

                        var PropertyAddress = propertyAddressModel.GetPropertyAddress(Item.Id);
                        var PropertyImages = propertyImgModel.GetPropertyImgs(Item.Id);

                        Property.Id = Item.Id;
                        Property.Title = Item.Title;
                        Property.EmployeeId = Item.EmployeeId;
                        Property.EmployeeName = Item.EmployeeName;
                        Property.Symbol = Item.Symbol;
                        Property.Currency = Item.Currency;
                        Property.Price = Item.Price;
                        Property.AreaLand = Item.AreaLand;
                        Property.AreaBuild = Item.AreaBuild;
                        Property.Description = Item.Description;
                        Property.PropertyTypeName = Item.PropertyTypeName;
                        Property.TransactionTypeName = Item.TransactionTypeName;
                        Property.PropertyStatus = Item.PropertyStatus;
                        Property.PropertyStatusName = Item.PropertyStatusName;
                        Property.CreationDate = Item.CreationDate;
                        Property.FullAddress = PropertyAddress.FullDirection;
                        Property.OtherSigns = PropertyAddress.OtherSigns;
                        Property.ImagesInfo = PropertyImages;
                        Properties.Add(Property);
                    }
                    
                    if (Session["Role"].ToString().AsInt() == 1)
                    {
                        return View(Properties);
                    }
                    else if (Session["Role"].ToString().AsInt() == 2)
                    {
                        if (Properties.Where(x => x.EmployeeId == Session["IdTEmployee"].ToString().AsInt()).ToList().Count > 0)
                        {
                            return View(Properties.Where(x => x.EmployeeId == Session["IdTEmployee"].ToString().AsInt()).ToList());
                        }
                        else
                        {
                            ViewBag.MensajeUsuario = "No tiene propiedades asignadas en este momento.";
                            return View();
                        }
                    }
                    else
                    {
                        ViewBag.MensajeUsuario = "No tiene permisos para ver esta información.";
                        return View();
                    }
                }
                else
                {
                    ViewBag.MensajeUsuario = "No hay propiedades registradas en el sistema.";
                    return View();
                }
            }
            catch (System.Exception)
            {
                return View();
            }
        }

        //Obtiene el listado de cantones de acuerdo a la provincia seleccionada
        [HttpGet]
        public ActionResult GetCantons(int q)
        {
            var cantons = propertyAddressModel.ListCantons(q);
            return Json(cantons, JsonRequestBehavior.AllowGet);
        }

        //Obtiene el listado de distritos de acuerdo al canton seleccionado
        [HttpGet]
        public ActionResult GetDistricts(int q)
        {
            var districts = propertyAddressModel.ListDistricts(q);
            return Json(districts, JsonRequestBehavior.AllowGet);
        }

        [SecurityModel]
        [HttpGet]
        public ActionResult ViewPropertyImages(int q)
        {
            PropertyEnt Property = new PropertyEnt();
            Property.Id = q;
            Property.ImagesInfo = propertyImgModel.GetPropertyImgs(q);
            return View(Property);
        }

        [HttpPost]
        public ActionResult AddPropertyImg(PropertyEnt entidad)
        {
            var result = SaveImages(entidad.Id, entidad);

            //PropertyEnt Property = new PropertyEnt();
            //Property.Id = entidad.Id;
            //Property.ImagesInfo = propertyImgModel.GetPropertyImgs(entidad.Id);

            if (result == true)
            {
                TempData["notification"] = "Se agregó la imagen correctamente.";
                TempData["notificationType"] = "success";
                return Json(new { success = true, message = TempData["notification"] + "|" + TempData["notificationType"], property = entidad.Id });
                //return View("ViewPropertyImages", Property);
            }
            else
            {
                //Property.Id = entidad.Id;
                //Property.ImagesInfo = propertyImgModel.GetPropertyImgs(entidad.Id);
                //return View(entidad.Id);
                TempData["notification"] = "Se produjo un error al registrar la imagen.";
                TempData["notificationType"] = "error";
                return Json(new { success = false, message = TempData["notification"] + "|" + TempData["notificationType"] });
            }
        }

        [SecurityModel]
        [HttpGet]
        public ActionResult ViewPropertyDocs(int q)
        {
            PropertyEnt Property = new PropertyEnt();
            Property.Id = q;
            Property.DocsInfo = propertyDocModel.GetPropertyDocs(q);
            return View(Property);
        }

        [HttpPost]
        public ActionResult AddPropertyDoc(PropertyEnt entidad)
        {
            var result = SaveDocs(entidad.Id, entidad);

            /*PropertyEnt Property = new PropertyEnt();
            Property.Id = entidad.Id;
            Property.DocsInfo = propertyDocModel.GetPropertyDocs(entidad.Id);*/

            if (result == true)
            {
                TempData["notification"] = "Se agregó el documento correctamente.";
                TempData["notificationType"] = "success";
                return Json(new { success = true, message = TempData["notification"] + "|" + TempData["notificationType"], property = entidad.Id });
                //return View("ViewPropertyDocs", Property);
            }
            else
            {
                /*Property.Id = entidad.Id;
                Property.ImagesInfo = propertyImgModel.GetPropertyImgs(entidad.Id);
                return View(Property);*/
                TempData["notification"] = "Se produjo un error al registrar el documento.";
                TempData["notificationType"] = "error";
                return Json(new { success = false, message = TempData["notification"] + "|" + TempData["notificationType"] });
            }

        }

        [HttpPost]
        public ActionResult DeletePropertyImg(int q, int p)
        {
            var resp = propertyImgModel.DeletePropertyImg(q);

            if (resp == 1)
            {
                /*PropertyEnt Property = new PropertyEnt();
                Property.Id = p;
                Property.ImagesInfo = propertyImgModel.GetPropertyImgs(p);
                return View("ViewPropertyImages", Property);*/
                TempData["notification"] = "Se eliminó la imagen correctamente.";
                TempData["notificationType"] = "success";
                return Json(new { success = true, message = TempData["notification"] + "|" + TempData["notificationType"], property = p });
                /*TempData["notification"] = "Se eliminó la imagen correctamente.";
                TempData["notificationType"] = "success";
                return Json(new { success = true, message = TempData["notification"] + "|" + TempData["notificationType"], property = p });*/
            }
            else
            {
                /*ViewBag.MensajeUsuario = "No se pudo eliminar la imagen";
                return View();*/
                /*Alert("Se produjo un error al eliminar la imagen.", AlertModel.NotificationType.error);
                return Json(new { success = false });*/
                TempData["notification"] = "Se produjo un error al eliminar la imagen.";
                TempData["notificationType"] = "error";
                return Json(new { success = false, message = TempData["notification"] + "|" + TempData["notificationType"] });
            }
        }

        [HttpPost]
        public ActionResult DeletePropertyDoc(int q, int p)
        {
            var resp = propertyDocModel.DeletePropertyDoc(q);

            if (resp == 1)
            {
                /*PropertyEnt Property = new PropertyEnt();
                Property.Id = p;
                Property.DocsInfo = propertyDocModel.GetPropertyDocs(p);
                return View("ViewPropertyDocs", Property);*/
                TempData["notification"] = "Se eliminó el documento correctamente.";
                TempData["notificationType"] = "success";
                return Json(new { success = true, message = TempData["notification"] + "|" + TempData["notificationType"], property = p });
            }
            else
            {
                /*ViewBag.MensajeUsuario = "No se pudo eliminar el documento";
                return View();*/
                TempData["notification"] = "Se produjo un error al eliminar el documento.";
                TempData["notificationType"] = "error";
                return Json(new { success = false, message = TempData["notification"] + "|" + TempData["notificationType"] });
            }
        }

        public bool SaveImages(int PropertyId, PropertyEnt entidad)
        {
            if (entidad.Images != null && entidad.Images.Count > 0 && entidad.Images[0] != null)
            {
                string extension = string.Empty;

                string rutaBase = Server.MapPath("~" + ImagesPath + PropertyId + "/");
                //AppDomain.CurrentDomain.BaseDirectory + "AppContent\\Properties\\Images\\Property" + PropertyId + "\\";

                string ruta = string.Empty;

                PropertyImgEnt propertyImg = new PropertyImgEnt();

                propertyImg.PropertyId = PropertyId;

                if (!Directory.Exists(rutaBase))
                {
                    Directory.CreateDirectory(rutaBase);
                }

                int cantidadArchivos = Directory.GetFiles(rutaBase).Length + 1;

                foreach (var item in entidad.Images)
                {
                    extension = Path.GetExtension(Path.GetFileName(item.FileName));
                    ruta = rutaBase + "Img-" + cantidadArchivos + extension;
                    item.SaveAs(ruta);

                    propertyImg.ImageUrl = ImagesPath + PropertyId + "/" + "Img-" + cantidadArchivos + extension;
                    var resp = propertyImgModel.RegisterPropertyImg(propertyImg);

                    if (resp == 1)
                    {
                        cantidadArchivos++;
                    }
                    else
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool SaveDocs(int PropertyId, PropertyEnt entidad)
        {
            if (entidad.Docs != null && entidad.Docs.Count > 0 && entidad.Docs[0] != null)
            {

                string extension = string.Empty;

                string rutaBase = Server.MapPath("~" + DocsPath + PropertyId + "/");
                //AppDomain.CurrentDomain.BaseDirectory + "AppContent\\Properties\\Docs\\Property" + PropertyId + "\\";

                string ruta = string.Empty;

                PropertyDocEnt propertyDoc = new PropertyDocEnt();

                propertyDoc.PropertyId = PropertyId;

                if (!Directory.Exists(rutaBase))
                {
                    Directory.CreateDirectory(rutaBase);
                }

                int cantidadArchivos = Directory.GetFiles(rutaBase).Length + 1;

                foreach (var item in entidad.Docs)
                {
                    //Tratar las imágenes de la propiedad
                    //Toma la extensión de la imagen
                    extension = Path.GetExtension(Path.GetFileName(item.FileName));
                    //Genera la ruta base de las carpetas

                    //Ruta donde se guardará la imagen
                    ruta = rutaBase + item.FileName;
                    //Guarda la imagen en la ruta especificada
                    item.SaveAs(ruta);

                    //Actualiza la ruta de la imagen en la base de datos
                    propertyDoc.Name = item.FileName;
                    propertyDoc.DocUrl = DocsPath + PropertyId + "/" + item.FileName;
                    //propertyImg.PropertyId = PropertyId;
                    var resp = propertyDocModel.RegisterPropertyDoc(propertyDoc);

                    if (resp == 1)
                    {
                        cantidadArchivos++;
                    }
                    else
                    {
                        return false;
                    }

                }
                return true;
            }
            else
            {
                return false;
            }
        }

        //A partir de aquí, se agregan los métodos para el filtro de propiedades
        public List<PropertyEnt> FilterPropertiesAccordingToCriteria(ViewPropertiesModel filters)
        {
            var filteredProperties = new List<PropertyEnt>();

            // Obtener todas las propiedades disponibles
            var allProperties = propertyModel.GetProperties();
            foreach (var prop in allProperties)
            {
                var address = propertyAddressModel.GetPropertyAddress(prop.Id);
                prop.Province = address.ProvinceId;
                prop.ProvinceName = address.Province;
                prop.Canton = address.CantonId;
                prop.CantonName = address.Canton;
                prop.District = address.DistrictId;
                prop.DistrictName = address.District;
                prop.FullAddress = address.FullDirection;
                prop.ImagesInfo = propertyImgModel.GetPropertyImgs(prop.Id);
            }

            // Aplicar los criterios de filtro uno por uno
            filteredProperties = ApplySearchTextFilter(allProperties, filters.Filter.SearchText);
            //filteredProperties = ApplyPriceRangeFilter(filteredProperties, filters.Filter.MinPrice, filters.Filter.MaxPrice);
            filteredProperties = ApplyPropertyTypeFilter(filteredProperties, filters.Filter.PropertyTypes);
            filteredProperties = ApplyTransactionTypeFilter(filteredProperties, filters.Filter.TransactionTypes);
            filteredProperties = ApplyProvinceFilter(filteredProperties, filters.Filter.Provinces);

            return filteredProperties;
        }

        public List<PropertyEnt> ApplySearchTextFilter(List<PropertyEnt> properties, string searchText)
        {
            if (string.IsNullOrEmpty(searchText))
                return properties;

            return properties.Where(p => p.Title.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
        }

        //public List<PropertyEnt> ApplyPriceRangeFilter(List<PropertyEnt> properties, decimal? minPrice, decimal? maxPrice)
        //{
        //    if (minPrice == null && maxPrice == null)
        //        return properties;

        //    if (minPrice == null)
        //        return properties.Where(p => p.Price <= maxPrice).ToList();

        //    if (maxPrice == null)
        //        return properties.Where(p => p.Price >= minPrice).ToList();

        //    return properties.Where(p => p.Price >= minPrice && p.Price <= maxPrice).ToList();
        //}

        public List<PropertyEnt> ApplyPropertyTypeFilter(List<PropertyEnt> properties, List<int> propertyTypes)
        {
            if (propertyTypes == null || propertyTypes.Count == 0)
                return properties;

            return properties.Where(p => propertyTypes.Contains(p.PropertyType)).ToList();
        }

        public List<PropertyEnt> ApplyTransactionTypeFilter(List<PropertyEnt> properties, List<int> transactionTypes)
        {
            if (transactionTypes == null || transactionTypes.Count == 0)
                return properties;

            return properties.Where(p => transactionTypes.Contains(p.TransactionType)).ToList();
        }

        public List<PropertyEnt> ApplyProvinceFilter(List<PropertyEnt> properties, List<int> provinces)
        {
            if (provinces == null || provinces.Count == 0)
                return properties;

            return properties.Where(p => provinces.Contains(p.Province)).ToList();
        }

        [SecurityModel]
        [HttpGet]
        public ActionResult DownloadFile(string fileName, int q)
        {
            var rutaArchivo = Path.Combine(Server.MapPath("~/AppContent/Properties/Docs/Property" + q), fileName);

            if (System.IO.File.Exists(rutaArchivo))
            {
                byte[] fileBytes = System.IO.File.ReadAllBytes(rutaArchivo);
                return File(fileBytes, MimeMapping.GetMimeMapping(fileName), fileName);
            }
            else
            {
                TempData["notification"] = "No se encuentra el archivo indicado.";
                TempData["notificationType"] = "error";
                return Json(new { success = false, message = TempData["notification"] + "|" + TempData["notificationType"] }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}