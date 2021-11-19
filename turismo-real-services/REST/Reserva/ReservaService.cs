using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using turismo_real_business.DTOs;
using turismo_real_services.Utils;

namespace turismo_real_services.REST.Reserva
{
    public class ReservaService
    {
        public List<ReservaDTO> GetReservas()
        {
            //try
            //{
                WebRequest request = WebRequest.Create(URLService.URL_RESERVAS);
                request.Method = "GET";
                request.PreAuthenticate = true;
                request.ContentType = "Application/json; Charset=UTF-8";
                request.Timeout = 3000;

                string result = "";
                HttpWebResponse httpResponse = (HttpWebResponse)request.GetResponse();
                using (StreamReader streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }
                dynamic response = JsonConvert.DeserializeObject(result);
                List<ReservaDTO> reservasResponse = ParseReservaResponse(response);
                return reservasResponse;
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //    return null;
            //}
        }

        public List<ReservaDTO> ParseReservaResponse(dynamic reservaREST)
        {
            List<ReservaDTO> reservas = new List<ReservaDTO>();
            foreach (dynamic reservaJSON in reservaREST)
            {
                ReservaDTO reserva = DynamicToReserva(reservaJSON);
                reservas.Add(reserva);
            }
            return reservas;
        }

        public ReservaDTO DynamicToReserva(dynamic reservaJSON)
        {
            ReservaDTO reserva = new ReservaDTO();
            reserva.idReserva = reservaJSON["idReserva"];
            reserva.fecHoraReserva = reservaJSON["fecHoraReserva"];
            reserva.fecDesde = reservaJSON["fecDesde"];
            reserva.fecHasta = reservaJSON["fecHasta"];
            reserva.valorArriendo = reservaJSON["valorArriendo"];
            reserva.fecHoraCheckIn = reservaJSON["fecHoraCheckIn"];
            reserva.fecHoraCheckOut = reservaJSON["fecHoraCheckOut"];
            reserva.estadoCheckIn = reservaJSON["estadoCheckIn"];
            reserva.estadoCheckOut = reservaJSON["estadoCheckOut"];
            reserva.estadoReserva = reservaJSON["estadoReserva"];
            reserva.idUsuario = reservaJSON["idUsuario"];
            reserva.idDepartamento = reservaJSON["idDepartamento"];
            reserva.servicios = GetServiciosReserva(reservaJSON["servicios"]);
            reserva.asistentes = GetAsistentesReserva(reservaJSON["asistentes"]);
            return reserva;
        }

        public List<AsistenteDTO> GetAsistentesReserva(dynamic asistentes)
        {
            List<AsistenteDTO> asistentesList = new List<AsistenteDTO>();
            foreach (dynamic asistente in asistentes)
            {
                AsistenteDTO asistenteReserva = new AsistenteDTO();
                asistenteReserva.pasaporte = asistente["pasaporte"];
                asistenteReserva.numRut = asistente["numRut"];
                asistenteReserva.dvRut = asistente["dvRut"];
                asistenteReserva.primerNombre = asistente["pNombre"];
                asistenteReserva.segundoNombre = asistente["sNombre"];
                asistenteReserva.primerApellido = asistente["pApellido"];
                asistenteReserva.primerApellido = asistente["sApellido"];
                asistenteReserva.correo = asistente["email"];
                asistentesList.Add(asistenteReserva);
            }
            return asistentesList;
        }

        private List<ServicioReservaDTO> GetServiciosReserva(dynamic servicios)
        {
            List<ServicioReservaDTO> serviciosList = new List<ServicioReservaDTO>();
            foreach (dynamic servicio in servicios)
            {
                ServicioReservaDTO servicioReserva = new ServicioReservaDTO();
                servicioReserva.id = servicio["id"];
                servicioReserva.idServicio = servicio["idServicio"];
                servicioReserva.servicio = servicio["servicio"];
                servicioReserva.tipo = servicio["tipo"];
                servicioReserva.valor = servicio["valor"];
                servicioReserva.conductor = servicio["conductor"];
                serviciosList.Add(servicioReserva);
            }
            return serviciosList;
        }

    }
}