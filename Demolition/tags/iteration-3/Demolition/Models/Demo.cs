using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Linq;
using System.Linq;
using Demolition.Workers;

namespace Demolition.Models
{
    [MetadataType(typeof(DemoValidator))]
    public partial class Demo : Model
    {
        private List<App> _AppsToStart;

        /// <summary>
        /// Booting = Starting up EC2 instance for this demo
        /// Launching = EC2 instance started, firing up apps
        /// Ready = all apps ready to go
        /// ShuttingDown = user wants to stop demo, EC2 instance still alive
        /// Terminated = EC2 instance stopped/killed
        /// </summary>
        public enum States { Booting, Launching, Ready, ShuttingDown, Terminated }

        public void Save(User user)
        {
            this.CreatedAt = DateTime.Now;
            this.UpdatedAt = DateTime.Now;
            this.State = States.Booting.ToString();
            this.UserID = user.Id;

            var context = GetDataContext();

            foreach (App app in AppsToStart.FindAll((a => a.Selected)))
            {
                    var instance = new Instance();
                    instance.AppID = app.Id;
                    instance.DemoID = this.Id;
                    instance.EC2State = "Booting";
                    instance.DataState = "Clean";
                    instance.CreatedAt = instance.UpdatedAt = DateTime.Now;
                this.Instances.Add(instance);
            }

            context.Demos.InsertOnSubmit(this);
            context.SubmitChanges();

            CreateInstanceWorker.Queue(this);
        }

        public void Destroy()
        {
            UpdateState(States.ShuttingDown);
            
            ShutDownDemoWorker.Queue(this);
        }

        public string StatusColor()
        {
            switch ((States)Enum.Parse(typeof(States), this.State))
            {
                case States.Ready:
                    return "green";
                case States.Booting:
                case States.Launching:
                    return "yellow";
                default:
                    return "red";
            }
        }

        public static IList<Demo> ListAll()
        {
            var demos = from d in GetDataContext().Demos
                        select d;
            return demos.ToList();
        }

        public static IList<Demo> ListByUser(User user)
        {
            var demos = from d in GetDataContext().Demos
                        where d.User.Name == user.Name
                        select d;

            return demos.ToList();
        }

        public static Demo Find(int id)
        {
            return GetDataContext().Demos.SingleOrDefault(d => d.Id == id);
        }

        public static Demo Create(string name, States state, int userId)
        {
            var demo = new Demo();
            demo.Name = name;
            demo.State = state.ToString();
            demo.UserID = userId;
            demo.CreatedAt = demo.UpdatedAt = DateTime.Now;

            var context = GetDataContext();
            context.Demos.InsertOnSubmit(demo);
            context.SubmitChanges();

            return demo;
        }

        public void UpdateState(States state)
        {
            this.State = state.ToString();

            var context = GetDataContext();
            var updatedInstance = context.Demos.SingleOrDefault(i => i.Id == this.Id);
            updatedInstance.State = this.State;
            context.SubmitChanges();
        }

        public void UpdateEC2Id(string EC2Id)
        {
            this.EC2Id = EC2Id;

            var context = GetDataContext();
            var updatedInstance = context.Demos.SingleOrDefault(i => i.Id == this.Id);
            updatedInstance.EC2Id = this.EC2Id;
            context.SubmitChanges();
        }

        public List<App> AppsToStart
        {
            get
            {
                if (_AppsToStart == null)
                    _AppsToStart = (List<App>)App.ListAll();
                return _AppsToStart;
            }
        }
    }
}