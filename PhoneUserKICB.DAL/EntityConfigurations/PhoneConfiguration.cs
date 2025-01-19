
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhoneUserKICB.DAL.Entities;

namespace PhoneUserKICB.DAL.EntityConfigurations;
public class PhoneConfiguration : IEntityTypeConfiguration<Phone>
{
    public void Configure(EntityTypeBuilder<Phone> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.PhoneNumber)
            .IsRequired()
            .HasMaxLength(15);
        builder.HasOne(p => p.User)
            .WithMany(u => u.Phones)
            .HasForeignKey(p => p.UserId);
    }
}
