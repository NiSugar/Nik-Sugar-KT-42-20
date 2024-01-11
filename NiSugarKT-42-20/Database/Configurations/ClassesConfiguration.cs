using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using NiSugarKT_42_20.Database.Helpers;
using NiSugarKT_42_20.Models;

namespace NiSugarKT_42_20.Database.Configurations
{
    public class ClassesConfiguration : IEntityTypeConfiguration<Classes>
    {
        private const string TableName = "cd_classes";

        public void Configure(EntityTypeBuilder<Classes> builder)
        {
            //Задаем первичный ключ
            builder
                .HasKey(p => p.ClassesId)
                .HasName($"pk_{TableName}_classes_id");

            //Для целочисленного первичного ключа задаем автогенерацию (к каждой новой записи будет добавлять +1)
            builder.Property(p => p.ClassesId)
                    .ValueGeneratedOnAdd();

            //Расписываем как будут называться колонки в БД, а так же их обязательность и тд
            builder.Property(p => p.ClassesId)
                .HasColumnName("lesson_id")
                .HasComment("Идентификатор записи предмета");

            //HasComment добавит комментарий, который будет отображаться в СУБД (добавлять по желанию)
            builder.Property(p => p.ClassesName)
                .IsRequired()
                .HasColumnName("c_lessonname")
                .HasColumnType(ColumnType.String).HasMaxLength(100)
                .HasComment("Название предмета");


        }
    }
}
