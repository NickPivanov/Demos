using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models
{
    
    public class Characteristic
    {
        [Key, LinqToDB.Mapping.Column("Id"), Identity]
        public int Id { get; set; }
        [LinqToDB.Mapping.Column("MotorId"), ForeignKey("FK_Characteristics_MotorId")]
        [NotNull]
        public int MotorId { get; set; }
        public Motor Motor { get; set; }
        [LinqToDB.Mapping.Column("MotorPropertyId"), ForeignKey("FK_Characteristics_PropertyId")]
        [NotNull]
        public int MotorPropertyId { get; set; }
        public MotorProperty MotorProperty { get; set; }
        [LinqToDB.Mapping.Column("Value"), NotNull]
        public double Value { get; set; }
    }
}
