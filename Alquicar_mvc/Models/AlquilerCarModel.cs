﻿using conexion;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Alquicar_mvc.Models
{
    public class AlquilerCarModel
    {
        [Display(Name = "Fecha de alquiler")]
        public string start_date { get; set; }

        [Display(Name = "Fecha de devolución")]
        public string final_date { get; set; }

        public string car_id { get; set; }

        public string cliente_id { get; set; }

        public string registrador_por { get; set; }

        Connection ConnAlquiler = new Connection();

        public System.Data.DataTable QueryClientes()
        {
            return ConnAlquiler.Query("PR_QUERYCLIENTES", null);
        }

        public System.Data.DataTable QueryVehiculos()
        {
            return ConnAlquiler.Query("PR_QUERYCARS", null);
        }

        public System.Data.DataTable QueryVehiculoAlquilado(string placa)
        {
            Parameter[] placacar = new Parameter[1];
            placacar[0] = new Parameter("_placa", placa);
            return ConnAlquiler.Query("PR_QUERYCARAlQUILER", placacar);
        }

        public System.Data.DataTable QueryAlquileres()
        {
            return ConnAlquiler.Query("PR_QUERYALQUILERES", null);
        }

        public bool RegisterDevolucioncar(string caralquilado)
        {
            Parameter[] data_alquiler = new Parameter[1];
            data_alquiler[0] = new Parameter("_idcar", caralquilado );
            Transaction[] trans = new Transaction[1];
            trans[0] = new Transaction("PR_UPTDCARDEVOLUCION", data_alquiler);
            return ConnAlquiler.Transaction(trans);
        }


        public bool RegisterAlquiler(AlquilerCarModel alquiler)
        {
            Parameter[] data_alquiler = new Parameter[5];
            data_alquiler[0] = new Parameter("_start_date", alquiler.start_date);
            data_alquiler[1] = new Parameter("_final_date", alquiler.final_date);
            data_alquiler[2] = new Parameter("_car_id", alquiler.car_id);
            data_alquiler[3] = new Parameter("_cliente_id", alquiler.cliente_id);
            data_alquiler[4] = new Parameter("_registrado_por", alquiler.registrador_por);

            Transaction[] trans = new Transaction[1];
            trans[0] = new Transaction("PR_ALQUILER", data_alquiler);
            return ConnAlquiler.Transaction(trans);
        }
    }
}