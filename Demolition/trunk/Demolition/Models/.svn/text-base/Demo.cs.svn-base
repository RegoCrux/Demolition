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
        public static readonly string CHECKSUM_QUERY = String.Format("select checksum_agg(binary_checksum(*)) from {0}.dbo.{1};", Properties.Settings.Default.MasterDB, Properties.Settings.Default.MasterTable);
        private IDictionary<string, bool> _AppsToStart;
        private IDictionary<string, bool> _IndustrySelected;

        /// <summary>
        /// Booting = Starting up EC2 instance for this demo
        /// Launching = EC2 instance started, firing up apps
        /// Ready = all apps ready to go
        /// ShuttingDown = user wants to stop demo, EC2 instance still alive
        /// Terminated = EC2 instance stopped/killed
        /// </summary>
        public enum States { Booting, Launching, Ready, ShuttingDown, Terminated, Error }

        public enum DataStates { Clean, Dirty }

        public void Save(User user)
        {
            this.CreatedAt = this.UpdatedAt = DateTime.Now;
            this.State = States.Booting.ToString();
            this.UserID = user.Id;
            this.DataState = DataStates.Clean.ToString();
            foreach (var indName in IndustrySelected)
            {
                this.IndustryID = Industry.Find(indName.Key).Id;
            }

            var context = GetDataContext();

            foreach (var appName in AppsToStart.Where(a => a.Value))
            {
                var instance = new Instance();
                instance.AppID = App.Find(appName.Key).Id;
                instance.DemoID = this.Id;
                instance.EC2State = States.Booting.ToString();
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

        public void ResetData()
        {
            this.UpdateDataState(DataStates.Clean);

            DataWorker.Queue(this);
        }

        public void UpdateDataState(DataStates newState)
        {
            this.DataState = newState.ToString();

            var context = GetDataContext();
            var demo = context.Demos.SingleOrDefault(i => i.Id == this.Id);
            demo.DataState = this.DataState;
            demo.UpdatedAt = DateTime.Now;
            context.SubmitChanges();
        }

        public void UpdateChecksum(int checksum)
        {
            var context = GetDataContext();
            var demo = context.Demos.SingleOrDefault(i => i.Id == this.Id);
            demo.Checksum = checksum;
            demo.UpdatedAt = DateTime.Now;
            context.SubmitChanges();
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

        public string DataStatusColor()
        {
            switch ((DataStates)Enum.Parse(typeof(DataStates), this.DataState))
            {
                case DataStates.Clean:
                    return "green";
                case DataStates.Dirty:
                    return "yellow";
                default:
                    return "red";
            }
        }

        public bool IsReady()
        {
            return this.State == States.Ready.ToString();
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

        public static Demo FindByName(string name)
        {
            return GetDataContext().Demos.SingleOrDefault(d => d.Name == name);
        }

        public static Demo Create(string name, States state, int userId)
        {
            var demo = new Demo();
            demo.Name = name;
            demo.State = state.ToString();
            demo.UserID = userId;
            demo.CreatedAt = demo.UpdatedAt = DateTime.Now;
            demo.DataState = DataStates.Clean.ToString();

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
            updatedInstance.UpdatedAt = DateTime.Now;
            context.SubmitChanges();
        }

        public void UpdateEC2Id(string EC2Id)
        {
            this.EC2Id = EC2Id;

            var context = GetDataContext();
            var updatedInstance = context.Demos.SingleOrDefault(i => i.Id == this.Id);
            updatedInstance.EC2Id = this.EC2Id;
            updatedInstance.UpdatedAt = DateTime.Now;
            context.SubmitChanges();
        }

        public IDictionary<string, bool> AppsToStart
        {
            get
            {
                if (_AppsToStart == null)
                {
                    _AppsToStart = new Dictionary<string, bool>();
                    foreach (var app in App.ListAll())
                        _AppsToStart.Add(app.Name, false);
                    
                }

                return _AppsToStart;
            }
        }

        public IDictionary<string, bool> IndustrySelected
        {
            get
            {
                if (_IndustrySelected == null)
                {
                    _IndustrySelected = new Dictionary<string, bool>();
                    foreach (var ind in Industry.ListAll())
                        _IndustrySelected.Add(ind.Name, false);

                }

                return _IndustrySelected;
            }
        }
    }
}