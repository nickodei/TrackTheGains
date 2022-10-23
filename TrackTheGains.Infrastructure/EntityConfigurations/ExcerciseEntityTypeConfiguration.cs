using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackTheGains.Domain.Models;

namespace TrackTheGains.Infrastructure.EntityConfigurations
{
    internal class ExcerciseEntityTypeConfiguration : IEntityTypeConfiguration<Excercise>
    {
        public void Configure(EntityTypeBuilder<Excercise> excerciseConfiguration)
        {
            excerciseConfiguration.ToTable("excercises", FitnessContext.DEFAULT_SCHEMA);

            excerciseConfiguration.HasKey(w => w.Id);
        }
    }
}
