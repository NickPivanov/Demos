using Domain.Models;
using LinqToDB;
using LinqToDB.Configuration;
using System;
using static LinqToDB.Reflection.Methods;

namespace DataAccess.ADONet
{
    public class ADOContext : LinqToDB.Data.DataConnection
    {
        public ADOContext(LinqToDbConnectionOptions<ADOContext> options) : base(options)
        {
        }

        public ITable<Motor> Motors => GetTable<Motor>();
        public ITable<Characteristic> Characteristics => GetTable<Characteristic>();
        public ITable<Measurement> Measurements => GetTable<Measurement>();
        public ITable<MotorProperty> MotorProperties => GetTable<MotorProperty>();
    }
}
