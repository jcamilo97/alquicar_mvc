﻿using conexion;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Alquicar_mvc.Models
{
    public class tipovehiculo {
        public string Id { get; set; }
        public string Nombre { get; set; }
    }
    public class tipo_direccion
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
    }

    public class CarMarca
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
    }
    public class RegisterCarModels
    {
        Connection registracar = new Connection();

        public string proveedor_car { get; set; }

        public string tipo_vehiculo { set; get; }

        public string tipo_direccion { set; get; }

        public string tipo_trasmicion { set; get; }
        
        public string marca { get; set; }
        //string gama { set; get; }
        public string color { set; get; }

        public string cilindraje { set; get; }

        public string modelo { set; get; }

        public string descripcion { set; get; }
        //string aire_acondicionado { set; get; }
        //string pasajeros { set; get; }
        //string maletero { set; get; }
        //string tipo_combustible { set; get; }
        public string placa { set; get; }

        public string kilomentros { set; get; }

        public string precio_pago { set; get; } 

        public string registrado_por { set; get; }
        //public string rutaImg { set; get; }
        //method return all types of car
        public SelectList get_tiposcar() {
            var tipovehiculolist = new List<tipovehiculo>();
            var Querylist = registracar.Query("PR_QERYTIPOSAUTOS", null);
            tipovehiculolist.Add(new tipovehiculo
            {
                Id ="",
                Nombre = ""
            });
            foreach (DataRow row in Querylist.Rows) {
                tipovehiculolist.Add(new tipovehiculo
                {
                   Id = row["id"].ToString(),
                    Nombre = row["auto_nombre"].ToString()
                });
            }
            var listVehiculos = new SelectList(tipovehiculolist, "Id", "Nombres");
            return listVehiculos;
        }

        //return all types of directions for cars
        public SelectList GetTipoDir() {
            var ListDir = new List<tipo_direccion>();
            var Querylist = registracar.Query("PR_QUERY_TIPO_DIR", null);
             ListDir.Add(new tipo_direccion
            {
                Id = "",
                Nombre = ""
            });
            foreach (DataRow row in Querylist.Rows) {
                ListDir.Add(new tipo_direccion() {
                    Id = row["id"].ToString(),
                    Nombre = row["tipo"].ToString()

                });
            }
            var list = new SelectList(ListDir, "Id", "Nombres");
            return list;
        }

        public SelectList GetTransmicionesmodel() {
            var ListDir = new List<tipo_direccion>();
            var Querylist = registracar.Query("PR_QUERYTRANSMITION", null);
            ListDir.Add(new tipo_direccion
            {
                Id = "",
                Nombre = ""
            });
            foreach (DataRow row in Querylist.Rows)
            {
                ListDir.Add(new tipo_direccion()
                {
                    Id = row["id"].ToString(),
                    Nombre = row["transmicion"].ToString()

                });
            }
            var list = new SelectList(ListDir, "Id", "Nombres");
            return list;
        }

        public List<CarMarca> GetMarcas() {
            var ListMarcas = new List<CarMarca>();
            var QueryMarcas = registracar.Query("PR_QUERY_MARCAS", null);
            ListMarcas.Add(new CarMarca {
                Id="",
                Nombre=""
            });
            foreach (DataRow row in QueryMarcas.Rows) {
                ListMarcas.Add(new CarMarca()
                {
                    Id = row["id"].ToString(),
                    Nombre = row["nombre"].ToString()
                });
            }
            //var lista = new SelectList(ListMarcas);
            return ListMarcas;
        }

        public System.Data.DataTable QueryMarcas() {
            return registracar.Query("PR_QUERY_MARCAS", null);
        }

        public System.Data.DataTable QueryTipoVehiculo()
        {
            return registracar.Query("PR_QERYTIPOSAUTOS", null);
        }

        public System.Data.DataTable QueryDireccion()
        {
            return registracar.Query("PR_QUERY_TIPO_DIR", null);
        }

        //types transmition
        public System.Data.DataTable QueryTransmition()
        {
            return registracar.Query("PR_QUERYTRANSMITION", null);
        }

        public System.Data.DataTable QueryProveedor()
        {
            return registracar.Query("PR_QUERYPROPIETARIO", null);
        }

        public System.Data.DataTable QueryCurrentCars()
        {
            return registracar.Query("PR_QUERYCARCURRENT", null);
        }

        public System.Data.DataTable QueryDetalleCar(int idcar)
        {
            Parameter[] detalleacar = new Parameter[1];
            detalleacar[0] = new Parameter("_idcar", Convert.ToString(idcar));
            return registracar.Query("PR_QUERYDETALLECAR", detalleacar);
        }

        public bool RegistrarCar(RegisterCarModels car, string rutaimg)
        {
            Parameter[] para_car = new Parameter[14];

            para_car[0] = new Parameter("_proveedorid", car.proveedor_car);
            para_car[1] = new Parameter("_tipo_vehiculo", car.tipo_vehiculo);
            para_car[2] = new Parameter("_tipo_direccion", car.tipo_direccion);
            para_car[3] = new Parameter("_tipo_transmicion", car.tipo_trasmicion);
            para_car[4] = new Parameter("_marca_id", car.marca);
            para_car[5] = new Parameter("_modelo", car.modelo);
            para_car[6] = new Parameter("_descripcion", car.descripcion);
            //para[4] = new Parameter("_gama", car.gama);
            para_car[7] = new Parameter("_color", car.color);
            para_car[8] = new Parameter("_cilindraje", car.cilindraje);
            //para[9] = new Parameter("_aire_acondicionado", car.aire_acondicionado);
            //para[10] = new Parameter("_pasajeros", car.pasajeros);
            //para[11] = new Parameter("_maletero", car.maletero);
            //para[12] = new Parameter("_car.tipo_combustible", car.tipo_combustible);
            para_car[9] = new Parameter("_placa", car.placa);
            para_car[10] = new Parameter("_ruta_img", rutaimg);
            para_car[11] = new Parameter("_km", car.kilomentros);
            para_car[12] = new Parameter("_precio_pago",car.precio_pago);
            para_car[13] = new Parameter("_registradopor", car.registrado_por);
            Transaction[] trans = new Transaction[1];
            trans[0] = new Transaction("PR_REGISTER_CAR", para_car);
            return registracar.Transaction(trans);

        }

    }
}