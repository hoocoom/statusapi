using System;
using System.Data.Entity;
using Status.DAL.Entities;

namespace Status.DAL
{
    public sealed class StatusContextInitializer : CreateDatabaseIfNotExists<StatusContext>
    {
        protected override void Seed(StatusContext context)
        {
            var states = Enum.GetValues(typeof (Core.Entities.Domain.State));
            
            foreach (Core.Entities.Domain.State state in states)
            {
                context.States.Add(new State() {StateId = state.ToString("G")});
            }

            context.StateLogs.Add(new StateLog()
            {
                StateId = Core.Entities.Domain.State.Ok.ToString("G"),
            });

            context.SaveChanges();
        }
    }
}