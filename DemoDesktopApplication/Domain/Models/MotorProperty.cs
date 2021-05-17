using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models
{
    public class MotorProperty
    {
        [Key, LinqToDB.Mapping.Column("Id"), Identity]
        public int Id { get; set; }

        [LinqToDB.Mapping.Column("Name")]
        public string Name { get; set; }

        [LinqToDB.Mapping.Column("MotorType")]
        public MotorType? MotorType { get; set; }

    }
}
