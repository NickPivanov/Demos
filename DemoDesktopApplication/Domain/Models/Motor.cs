using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models
{
    public class Motor
    {
        [Key, LinqToDB.Mapping.Column("Id"), Identity]
        public int Id { get; set; }
        [LinqToDB.Mapping.Column("Name"), NotNull]
        [MaxLength(50)]
        public string Name { get; set; }
        [LinqToDB.Mapping.Column("Type"), NotNull]
        public MotorType Type { get; set; }
        //public int MeasurementId и CharacteristicsId не должно быть, потому что Measurements для одного Motor больше одного
        public IEnumerable<Characteristic> Characteristics { get; set; }
        public IEnumerable<Measurement> MeasurementsLog { get; set; }
    }
}
