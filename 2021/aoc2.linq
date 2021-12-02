<Query Kind="Program">
  <NuGetReference>Center.DataAccess</NuGetReference>
  <NuGetReference>Center.DataAccess.Erp</NuGetReference>
  <NuGetReference>Center.Services.Messaging</NuGetReference>
  <NuGetReference>Moq</NuGetReference>
  <Namespace>Center.Common.Infrastructure</Namespace>
  <Namespace>Center.DataAccess</Namespace>
  <Namespace>Center.DataAccess.Attributes</Namespace>
  <Namespace>Center.DataAccess.CustomTypes</Namespace>
  <Namespace>Center.DataAccess.Domain</Namespace>
  <Namespace>Center.DataAccess.Domain.Auth</Namespace>
  <Namespace>Center.DataAccess.Domain.Auth.Mapping</Namespace>
  <Namespace>Center.DataAccess.Domain.Base</Namespace>
  <Namespace>Center.DataAccess.Domain.Base.Mapping</Namespace>
  <Namespace>Center.DataAccess.Domain.Edifact</Namespace>
  <Namespace>Center.DataAccess.Domain.Edifact.Mapping</Namespace>
  <Namespace>Center.DataAccess.Domain.EmailNotification</Namespace>
  <Namespace>Center.DataAccess.Domain.EmailNotification.Mapping</Namespace>
  <Namespace>Center.DataAccess.Domain.ExternalData</Namespace>
  <Namespace>Center.DataAccess.Domain.ExternalData.Mapping</Namespace>
  <Namespace>Center.DataAccess.Domain.Ias</Namespace>
  <Namespace>Center.DataAccess.Domain.Ias.Mapping</Namespace>
  <Namespace>Center.DataAccess.Domain.Masterplan</Namespace>
  <Namespace>Center.DataAccess.Domain.Masterplan.Mapping</Namespace>
  <Namespace>Center.DataAccess.Domain.Reporting</Namespace>
  <Namespace>Center.DataAccess.Domain.Reporting.Mapping</Namespace>
  <Namespace>Center.DataAccess.Domain.Yard</Namespace>
  <Namespace>Center.DataAccess.Domain.Yard.Enum</Namespace>
  <Namespace>Center.DataAccess.Erp</Namespace>
  <Namespace>Center.DataAccess.Erp.Domain</Namespace>
  <Namespace>Center.Messaging</Namespace>
  <Namespace>ITPartner.Logic</Namespace>
  <Namespace>ITPartner.Logic.Log.Implementation</Namespace>
  <Namespace>RabbitMQ.Client</Namespace>
  <Namespace>System.Collections.Concurrent</Namespace>
  <Namespace>System.Data.Entity</Namespace>
  <Namespace>System.Data.Entity.Core</Namespace>
  <Namespace>System.Data.Entity.Core.Common</Namespace>
  <Namespace>System.Data.Entity.Core.Common.CommandTrees</Namespace>
  <Namespace>System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder</Namespace>
  <Namespace>System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder.Spatial</Namespace>
  <Namespace>System.Data.Entity.Core.Common.EntitySql</Namespace>
  <Namespace>System.Data.Entity.Core.EntityClient</Namespace>
  <Namespace>System.Data.Entity.Core.Mapping</Namespace>
  <Namespace>System.Data.Entity.Core.Metadata.Edm</Namespace>
  <Namespace>System.Data.Entity.Core.Objects</Namespace>
  <Namespace>System.Data.Entity.Core.Objects.DataClasses</Namespace>
  <Namespace>System.Data.Entity.Infrastructure</Namespace>
  <Namespace>System.Data.Entity.Infrastructure.Annotations</Namespace>
  <Namespace>System.Data.Entity.Infrastructure.DependencyResolution</Namespace>
  <Namespace>System.Data.Entity.Infrastructure.Design</Namespace>
  <Namespace>System.Data.Entity.Infrastructure.Interception</Namespace>
  <Namespace>System.Data.Entity.Infrastructure.MappingViews</Namespace>
  <Namespace>System.Data.Entity.Infrastructure.Pluralization</Namespace>
  <Namespace>System.Data.Entity.Migrations</Namespace>
  <Namespace>System.Data.Entity.Migrations.Builders</Namespace>
  <Namespace>System.Data.Entity.Migrations.Design</Namespace>
  <Namespace>System.Data.Entity.Migrations.History</Namespace>
  <Namespace>System.Data.Entity.Migrations.Infrastructure</Namespace>
  <Namespace>System.Data.Entity.Migrations.Model</Namespace>
  <Namespace>System.Data.Entity.Migrations.Sql</Namespace>
  <Namespace>System.Data.Entity.Migrations.Utilities</Namespace>
  <Namespace>System.Data.Entity.ModelConfiguration</Namespace>
  <Namespace>System.Data.Entity.ModelConfiguration.Configuration</Namespace>
  <Namespace>System.Data.Entity.ModelConfiguration.Conventions</Namespace>
  <Namespace>System.Data.Entity.Spatial</Namespace>
  <Namespace>System.Data.Entity.SqlServer</Namespace>
  <Namespace>System.Data.Entity.SqlServer.Utilities</Namespace>
  <Namespace>System.Data.Entity.Utilities</Namespace>
  <Namespace>System.Data.Entity.Validation</Namespace>
</Query>

void Main()
{
   var data = INPUT.Split(new string[] {  Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).Select(input => Record.Parse(input.Trim())).ToList();
   
  Resolve_2_2(data);
      
}

void Resolve_2_2(List<Record> data)
{

   int forward = 0, depth = 0, aim = 0;
   foreach (Record element in data)
   {
      switch (element.Direction)
      {
         case Direction.Forward:
            forward += element.Steps;
            depth += (aim*element.Steps);
            break;
         case Direction.Up:
            aim  -= element.Steps;
            break;
         case Direction.Down:
            aim += element.Steps;
            break;
      }
   }

   new { forward, depth, total = forward*depth }.Dump("2.2");

}
void Resolve_2_1(List<Record> data)
{

   int forward = 0, depth = 0;
   foreach (Record element in data)
   {
      switch (element.Direction)
      {
         case Direction.Forward:
            forward += element.Steps;
            break;
         case Direction.Up:
            depth -= element.Steps;
            break;
         case Direction.Down:
            depth += element.Steps;
            break;
      }
   }

   new { forward, depth, total = forward * depth }.Dump("2.1");

}



class Record
{
   public Direction Direction { get; set; }
   public int Steps { get; set; }

   public static Record Parse(string input)
   {
      Direction dir = input.StartsWith("f") ? Direction.Forward
         : input.StartsWith("u")
            ? Direction.Up
            : input.StartsWith("d")
            ? Direction.Down
               : throw new Exception(input);

      return new Record
      {
         Direction = dir,
         Steps = int.Parse(input.Split(' ')[1])
      };

   }
}

enum Direction {
   Forward,
   Down,
   Up
}

// Define other methods and classes here
const string INPUT = @"forward 3
down 9
forward 5
up 1
forward 2
down 1
down 7
down 5
up 6
forward 3
down 6
forward 9
down 6
forward 2
down 4
forward 4
down 9
down 7
down 2
down 4
forward 3
forward 6
down 3
up 1
down 5
down 8
down 1
forward 9
forward 4
forward 3
down 3
down 6
down 3
up 2
down 3
down 9
down 1
down 9
up 8
down 1
down 9
forward 9
forward 2
down 1
forward 2
down 9
forward 9
up 7
forward 1
up 8
forward 7
forward 6
forward 2
down 8
forward 7
down 3
down 2
down 1
forward 2
down 6
forward 8
down 7
forward 9
down 7
down 9
forward 2
forward 2
up 3
down 4
down 8
forward 5
down 4
down 8
down 2
up 7
down 7
up 9
up 9
up 1
forward 2
up 4
forward 5
forward 9
forward 9
forward 3
down 6
up 3
down 1
forward 8
forward 2
down 7
forward 9
forward 1
forward 8
forward 8
down 2
down 6
forward 8
forward 8
forward 3
forward 4
down 3
up 3
forward 1
forward 4
down 1
down 4
down 2
down 3
forward 5
down 3
up 5
forward 9
down 8
up 6
down 6
up 7
up 7
forward 1
forward 7
down 1
up 3
down 1
forward 7
forward 1
forward 9
down 2
forward 9
down 3
down 5
forward 2
up 3
forward 5
forward 5
down 8
down 7
forward 6
down 2
down 5
up 4
up 5
down 6
forward 5
down 3
down 8
forward 7
down 5
down 5
down 9
down 9
down 2
down 7
up 4
forward 8
up 6
down 5
forward 1
up 2
down 6
up 8
up 7
down 6
forward 4
down 6
up 6
up 4
forward 5
forward 4
forward 6
down 3
down 7
down 9
forward 2
forward 6
down 3
forward 1
forward 2
forward 9
up 5
down 7
down 6
forward 2
forward 1
up 3
down 8
forward 9
down 7
forward 7
up 2
up 8
up 8
forward 7
forward 5
forward 9
down 7
down 7
forward 5
forward 4
forward 2
forward 8
up 3
up 7
forward 8
forward 6
forward 2
forward 6
up 3
up 1
forward 6
forward 9
down 1
forward 6
forward 4
up 6
forward 1
down 7
forward 7
up 5
down 5
down 3
forward 4
forward 6
up 6
forward 9
forward 2
down 7
forward 9
down 9
forward 2
up 1
down 4
forward 6
forward 4
down 6
forward 1
up 3
up 5
down 8
forward 2
up 7
down 5
down 2
down 6
forward 7
down 8
up 8
down 7
down 9
down 7
down 8
down 4
up 3
up 9
down 4
forward 7
down 5
up 8
down 3
forward 8
down 3
down 4
down 1
forward 5
down 4
down 8
up 7
forward 2
forward 8
down 1
down 3
forward 4
forward 5
forward 8
forward 1
down 1
down 9
up 8
forward 6
down 8
down 2
forward 9
down 5
down 8
up 8
up 5
forward 9
up 6
down 9
up 1
down 2
down 4
forward 9
forward 1
up 2
down 7
forward 9
down 9
down 6
down 9
down 8
forward 7
forward 6
forward 9
forward 9
forward 8
forward 5
up 2
forward 9
forward 2
down 1
down 1
down 5
down 1
down 7
up 2
up 7
forward 7
forward 8
down 2
down 2
down 3
up 8
up 8
up 3
forward 3
down 7
up 4
up 8
down 5
forward 4
forward 7
down 9
up 7
forward 8
forward 5
forward 8
forward 8
forward 6
forward 5
forward 2
down 3
up 2
forward 6
forward 5
down 9
down 2
down 7
down 2
forward 2
forward 6
forward 8
down 7
forward 4
down 3
down 5
forward 1
down 9
forward 5
down 4
forward 9
down 5
down 4
down 4
down 7
forward 9
down 3
down 5
down 6
down 4
forward 4
down 4
up 1
down 4
up 7
forward 4
forward 5
up 9
down 4
up 9
forward 9
down 8
down 1
up 7
down 4
up 4
forward 9
down 9
down 4
up 4
down 5
forward 2
up 4
down 3
forward 9
forward 8
down 2
forward 5
up 5
down 9
down 7
down 5
down 9
down 1
down 7
down 2
forward 4
up 7
forward 7
down 8
down 2
down 8
up 6
down 7
down 7
forward 3
up 3
forward 6
down 8
down 3
up 2
down 9
forward 3
down 9
down 6
up 8
forward 5
down 9
up 2
up 8
down 8
up 1
up 2
forward 5
up 3
down 7
forward 4
forward 2
up 1
forward 2
up 1
down 1
down 5
forward 6
up 2
down 7
down 8
down 9
up 9
down 2
up 2
forward 9
forward 6
forward 5
down 6
up 6
forward 6
forward 3
down 3
forward 2
forward 4
forward 1
down 9
forward 3
forward 2
down 5
up 2
forward 7
down 4
forward 5
down 4
forward 2
down 4
up 3
forward 6
forward 9
down 1
forward 2
up 8
forward 4
up 9
up 4
up 3
forward 5
down 7
forward 2
up 4
forward 7
down 8
forward 6
forward 4
up 5
down 4
down 6
down 3
forward 6
down 9
up 6
forward 3
down 4
forward 8
forward 1
down 3
down 4
up 4
forward 1
up 5
up 9
forward 4
up 9
forward 2
up 5
up 5
up 7
up 4
down 3
forward 8
forward 1
up 1
down 8
up 3
up 4
up 2
up 8
up 7
down 8
up 8
forward 9
down 8
up 5
forward 6
forward 4
down 8
down 9
down 4
down 6
forward 4
up 6
up 1
forward 7
up 4
down 6
up 3
down 4
forward 8
forward 4
up 2
down 3
up 3
up 9
down 4
forward 4
forward 5
forward 2
down 1
down 6
down 1
forward 6
down 2
forward 1
down 2
down 4
forward 1
down 8
up 2
down 5
forward 9
forward 4
down 9
forward 8
forward 2
forward 7
forward 1
forward 1
down 8
forward 2
forward 8
forward 7
forward 9
down 4
down 2
forward 1
forward 2
down 1
forward 1
forward 5
down 1
down 5
down 1
forward 2
up 9
forward 2
forward 4
down 9
up 7
down 1
up 4
forward 9
up 6
up 8
down 3
forward 9
up 6
down 1
forward 9
forward 3
up 5
forward 9
down 1
forward 5
up 5
down 1
up 4
forward 3
forward 1
up 4
forward 3
forward 9
down 2
forward 5
forward 4
forward 9
down 5
forward 8
forward 1
down 3
down 2
down 3
up 8
forward 3
forward 6
up 8
down 6
forward 8
forward 1
down 8
down 7
forward 8
down 2
forward 8
down 4
forward 1
down 1
up 6
forward 1
up 7
down 2
forward 5
up 9
down 5
forward 4
down 6
down 9
forward 8
up 2
up 7
forward 2
forward 5
up 9
down 4
forward 9
down 4
down 3
down 6
down 9
down 9
down 1
down 1
down 7
down 4
down 7
up 5
forward 6
down 9
forward 7
down 5
down 4
down 2
down 4
down 9
forward 1
down 9
down 8
forward 2
up 7
up 3
forward 9
forward 4
down 8
down 4
forward 2
down 8
up 3
forward 6
forward 4
down 2
up 9
down 5
up 8
up 6
up 3
down 2
forward 6
forward 4
forward 7
forward 2
down 5
down 2
forward 2
forward 6
down 5
down 4
forward 8
up 3
forward 7
down 1
forward 5
down 8
down 9
forward 5
down 7
forward 7
up 6
down 3
forward 1
down 2
down 9
down 2
down 1
forward 4
up 5
up 9
forward 1
down 5
forward 4
up 3
up 5
forward 7
forward 5
down 2
down 8
forward 5
down 7
up 8
down 5
down 6
forward 8
forward 9
down 8
up 3
down 8
down 2
forward 8
forward 8
forward 4
forward 9
up 7
up 1
down 5
down 8
down 5
forward 3
forward 2
down 8
down 3
down 2
down 5
forward 8
up 3
down 9
up 4
up 1
up 8
down 8
forward 5
down 2
forward 4
forward 1
down 7
forward 4
forward 5
up 2
down 6
up 9
forward 1
down 9
forward 4
down 7
down 9
up 9
forward 2
forward 7
down 7
forward 9
forward 1
forward 1
down 7
up 6
up 3
forward 2
forward 6
forward 9
forward 3
forward 4
forward 9
forward 9
forward 9
down 8
up 2
forward 7
down 8
down 3
up 8
down 8
forward 1
forward 9
forward 2
forward 3
down 8
forward 1
forward 4
down 9
down 4
up 7
forward 5
down 4
forward 5
down 2
forward 6
down 1
up 9
down 5
up 5
down 2
up 1
up 8
down 3
up 3
down 8
forward 4
forward 1
up 5
forward 1
down 5
up 5
forward 8
down 1
up 4
forward 9
forward 7
up 1
up 9
forward 7
forward 1
up 5
forward 6
down 2
up 5
down 4
down 6
down 3
forward 8
down 7
down 5
down 7
forward 1
down 7
up 5
down 4
down 4
down 4
forward 3
forward 4
up 6
forward 8
forward 2
up 1
forward 5
forward 6
forward 6
up 2
down 3
forward 3
up 8
forward 6
forward 5
up 2
up 5
down 6
down 8
down 1
forward 6
down 3
down 2
forward 4
down 4
down 7
forward 9
forward 4
forward 5
down 8
down 9
up 4
up 4
down 5
up 1
up 6
down 9
forward 9
forward 4
forward 9
forward 9
down 5
down 1
up 9
down 3
up 5
down 7
forward 6
forward 2
down 5
down 6
forward 7
forward 2
up 9
forward 6
down 7
up 4
forward 1
down 5
forward 2
forward 1
down 6
down 1
down 4
forward 8
forward 1
down 5
down 8
down 3
forward 4
down 2
forward 9
up 1
forward 8
down 4
down 3
down 1
forward 5
forward 9
down 3
forward 6
up 6
up 9
forward 8
forward 2
down 9
forward 3
down 4
down 5
down 4
forward 2
forward 6
down 9
down 5
forward 6
forward 3
forward 5
forward 6
forward 5
forward 1
up 4
up 1
down 2
up 6
down 5
down 1
forward 9
down 1
down 2
forward 6
up 2
down 4
up 3
forward 8
down 4
down 4
down 6
up 1
down 7
up 4
down 6
up 7
up 6
down 5
forward 3
forward 4
up 5
down 2
down 9
forward 9";