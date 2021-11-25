using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainReservation.DBOperations;
using TrainReservation.Model;
using System.Text.Json;

namespace TrainReservation.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class ReservationController:ControllerBase
    {
        private readonly TrainDbContext db;
        public ReservationController(TrainDbContext context)
        {
            db = context;
        }
        [HttpPost]
        
       /* public IActionResult AddReservation(Boolean IsSameWagon,int Passengers,string name)
        {
            ResponseModel res = new ResponseModel();
            
            List<ResponseWagon> reswag = new List<ResponseWagon>();
            //var db2 = new TrainDbContext();
            using (var db = new TrainDbContext())
            {
                var train = db.Trains.Where(i => i.Name == name).FirstOrDefault();
                var wagons = db.Wagons.Where(i => i.TrainId == train.Id).ToList();
                train.Wagons = wagons;
                



                if (train is not null)
                {
                    
                    
                    if (IsSameWagon)
                    {
                        foreach (var wagon in train.Wagons)
                        {
                            
                            if ((Passengers+wagon.Occupancy)<=(wagon.Capacity*0.7))
                            {
                                wagon.Occupancy += Passengers;
                                db.SaveChanges();
                                res.wagons.Add(new ResponseWagon { WagonName = wagon.Name, PersonCount = Passengers });
                                string json = JsonSerializer.Serialize(train)+ JsonSerializer.Serialize(res);

                                return Ok(json);
                            }
                            
                        }
                    }
                    else
                    {

                        res.IsRezervation = true;
                        foreach (var wagon in train.Wagons)
                        {
                            Console.WriteLine(1);
                            int tmp = Convert.ToInt32((wagon.Capacity * 0.7)) - wagon.Occupancy;//bu kadar yolcu alabilir bu vagon
                            //passenger benim yolcu saysısı
                            tmp = (tmp >= Passengers) ? Passengers : tmp;
                            
                            if (Passengers>0)
                            {
                                if (tmp>0)
                                {
                                    
                                    
                                    Passengers -= tmp;
                                    wagon.Occupancy += tmp;
                                    reswag.Add(new ResponseWagon { WagonName = wagon.Name, PersonCount = tmp });
   
                                    
           
                                   
                                }
                                
                                
                                
                            }
                            else
                            {
                                res.wagons = reswag;
                                db.SaveChanges();

                                return Ok(res);
                            }

                        }
                        if (Passengers==0)
                        {
                            res.wagons = reswag;
                            db.SaveChanges();

                            return Ok(res);
                        }
                        
                    }
                    
                }
            }
            res.IsRezervation = false;
            res.wagons = null;
            return BadRequest(res);
        }*/
        [HttpPost]
        public IActionResult AddReservation0(RequestModel TrainR)
        {
            ResponseModel res = new ResponseModel();

            List<ResponseWagon> reswag = new List<ResponseWagon>();
            var NewTrain = new Train();//db.Trains.Where(i => i.Name == TrainR.Name).FirstOrDefault();
            /*if (NewTrain is null)
            {
                NewTrain = new Train();
            }
            */

            var NewWagon = new Wagon();
            if (TrainR is not null)
            {
                

                if (!TrainR.DifferentWagon)
                {
                    foreach (var wagon in TrainR.Wagons)
                    {
                        if ((TrainR.NumberOfPersonsToReservation + wagon.Occupancy) <= (wagon.Capacity * 0.7))
                        {
                            res.IsRezervation = true;
                            wagon.Occupancy += TrainR.NumberOfPersonsToReservation;
                            NewWagon.Capacity = wagon.Capacity;
                            NewWagon.Name = wagon.Name;
                            NewWagon.Occupancy = wagon.Occupancy;
                            NewTrain.Wagons.Add(NewWagon);

                            NewTrain.Name = TrainR.Name;
                            db.Trains.Add(NewTrain);
                            db.SaveChanges();
                            res.wagons.Add(new ResponseWagon { WagonName = wagon.Name, PersonCount = TrainR.NumberOfPersonsToReservation });
                            string json =JsonSerializer.Serialize(res);
                            return Ok(json);
                        }
                    }
                }
                else
                {

                    res.IsRezervation = true;
                    foreach (var wagon in TrainR.Wagons)
                    {
                        
                        int tmp = Convert.ToInt32((wagon.Capacity * 0.7)) - wagon.Occupancy;//bu kadar yolcu alabilir bu vagon
                                                                                            //passenger benim yolcu saysısı
                        tmp = (tmp >= TrainR.NumberOfPersonsToReservation) ? TrainR.NumberOfPersonsToReservation : tmp;

                        if (TrainR.NumberOfPersonsToReservation > 0)
                        {
                            if (tmp > 0)
                            {

                                res.IsRezervation = true;
                                wagon.Occupancy += TrainR.NumberOfPersonsToReservation;
                                NewWagon.Capacity = wagon.Capacity;
                                NewWagon.Name = wagon.Name;
                                NewWagon.Occupancy = wagon.Occupancy;
                                NewTrain.Wagons.Add(NewWagon);

                                
                                
                                TrainR.NumberOfPersonsToReservation -= tmp;
                                wagon.Occupancy += tmp;
                                reswag.Add(new ResponseWagon { WagonName = wagon.Name, PersonCount = tmp });




                            }



                        }
                        else
                        {
                            res.wagons = reswag;
                            NewTrain.Name = TrainR.Name;
                            db.Trains.Add(NewTrain);
                            db.SaveChanges();

                            return Ok(res);
                        }

                    }
                    if (TrainR.NumberOfPersonsToReservation == 0)
                    {
                        res.wagons = reswag;
                        db.Trains.Add(NewTrain);
                        db.SaveChanges();

                        return Ok(res);
                    }

                }
            }


            res.IsRezervation = false;
            res.wagons = null;
            return BadRequest(res);
        }

        [HttpGet]
        public IActionResult GetTrain()
        {
            var train = db.Trains.ToList();
            var wagons = db.Wagons.ToList();

            string json = JsonSerializer.Serialize(train);
            return Ok(json);
        }
       
    }
    
    public class ResponseModel
    {
        public ResponseModel()
        {
            this.wagons = new List<ResponseWagon>();
        }
        public bool IsRezervation { get; set; }
        public List<ResponseWagon> wagons { get; set; }
    }
    public class RequestModel
    {
        public RequestModel()
        {
            this.Wagons = new List<RequestWagon>();
        }
        public string Name { get; set; }
        public List<RequestWagon> Wagons { get; set; }
        public int NumberOfPersonsToReservation { get; set; }
        public bool DifferentWagon { get; set; }

    }
    public class RequestWagon
    {
        public string Name { get; set; }
        public int Capacity { get; set; }
        public int Occupancy { get; set; }


    }
    public class ResponseWagon
    {
        public string WagonName { get; set; }
        public int PersonCount { get; set; }

    }
}
