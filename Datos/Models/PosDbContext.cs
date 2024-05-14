using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Datos.Models;

public partial class PosDbContext : DbContext
{
    public PosDbContext()
    {
    }

    public PosDbContext(DbContextOptions<PosDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblCliente> TblClientes { get; set; }

    public virtual DbSet<TblEmpleado> TblEmpleados { get; set; }

    public virtual DbSet<TblEmpresa> TblEmpresas { get; set; }

    public virtual DbSet<TblProducto> TblProductos { get; set; }

    public virtual DbSet<TblProductoSucursal> TblProductoSucursals { get; set; }

    public virtual DbSet<TblSucursal> TblSucursals { get; set; }

    public virtual DbSet<TblTelefonoEmpresa> TblTelefonoEmpresas { get; set; }

    public virtual DbSet<TblTelefonoSucursal> TblTelefonoSucursals { get; set; }

    public virtual DbSet<TblVentaDetalle> TblVentaDetalles { get; set; }

    public virtual DbSet<TblVentum> TblVenta { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(local);Database=pos_db;User Id=lorenzo;Password=123;Encrypt=False;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblCliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente);

            entity.ToTable("tbl_cliente");

            entity.HasIndex(e => e.RfcCliente, "cliente_rfc_unique").IsUnique();

            entity.Property(e => e.IdCliente).HasColumnName("id_cliente");
            entity.Property(e => e.ApellidoMaterno)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("apellido_materno");
            entity.Property(e => e.ApellidoPaterno)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("apellido_paterno");
            entity.Property(e => e.CalleDireccionCliente)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("calle_direccion_cliente");
            entity.Property(e => e.Descuento)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("descuento");
            entity.Property(e => e.Eliminado).HasColumnName("eliminado");
            entity.Property(e => e.IdEmpleado).HasColumnName("id_empleado");
            entity.Property(e => e.IdEmpresa).HasColumnName("id_empresa");
            entity.Property(e => e.NombreCliente)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre_cliente");
            entity.Property(e => e.NumeroDirecionCiente).HasColumnName("numero_direcion_ciente");
            entity.Property(e => e.RfcCliente)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("rfc_cliente");
            entity.Property(e => e.TipoCliente)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("tipo_cliente");

            entity.HasOne(d => d.IdEmpresaNavigation).WithMany(p => p.TblClientes)
                .HasForeignKey(d => d.IdEmpresa)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbl_cliente_tbl_empresa");
        });

        modelBuilder.Entity<TblEmpleado>(entity =>
        {
            entity.HasKey(e => e.IdEmpleado);

            entity.ToTable("tbl_empleado");

            entity.Property(e => e.IdEmpleado).HasColumnName("id_empleado");
            entity.Property(e => e.ApellidoMaterno)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("apellido_materno");
            entity.Property(e => e.ApellidoPaterno)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("apellido_paterno");
            entity.Property(e => e.ContrasenaSistema)
                .HasMaxLength(36)
                .IsFixedLength()
                .HasColumnName("contrasena_sistema");
            entity.Property(e => e.Eliminado).HasColumnName("eliminado");
            entity.Property(e => e.IdSucursal).HasColumnName("id_sucursal");
            entity.Property(e => e.NombreEmpleado)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre_empleado");
            entity.Property(e => e.Nss)
                .HasMaxLength(11)
                .IsFixedLength()
                .HasColumnName("nss");
            entity.Property(e => e.TipoUsuario)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tipo_usuario");
            entity.Property(e => e.UsuarioSistema)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("usuario_sistema");

            entity.HasOne(d => d.IdSucursalNavigation).WithMany(p => p.TblEmpleados)
                .HasForeignKey(d => d.IdSucursal)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbl_empleado_tbl_sucursal");
        });

        modelBuilder.Entity<TblEmpresa>(entity =>
        {
            entity.HasKey(e => e.IdEmpresa);

            entity.ToTable("tbl_empresa");

            entity.HasIndex(e => e.CiudadEmpresa, "ciudad_unique").IsUnique();

            entity.Property(e => e.IdEmpresa).HasColumnName("id_empresa");
            entity.Property(e => e.CalleDireccionEmpresa)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("calle_direccion_empresa");
            entity.Property(e => e.CiudadEmpresa)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("ciudad_empresa");
            entity.Property(e => e.Eliminado).HasColumnName("eliminado");
            entity.Property(e => e.NumeroDireccionEmpresa).HasColumnName("numero_direccion_empresa");
            entity.Property(e => e.RazonSocialEmpresa)
                .HasMaxLength(30)
                .IsFixedLength()
                .HasColumnName("razon_social_empresa");
            entity.Property(e => e.RfcEmpresa)
                .HasMaxLength(13)
                .IsFixedLength()
                .HasColumnName("rfc_empresa");
        });

        modelBuilder.Entity<TblProducto>(entity =>
        {
            entity.HasKey(e => e.IdProducto);

            entity.ToTable("tbl_producto");

            entity.Property(e => e.IdProducto).HasColumnName("id_producto");
            entity.Property(e => e.DescripcionProducto)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("descripcion_producto");
            entity.Property(e => e.Eliminado).HasColumnName("eliminado");
            entity.Property(e => e.FolioProducto)
                .HasMaxLength(10)
                .HasColumnName("folio_producto");
            entity.Property(e => e.NombreProducto)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("nombre_producto");
            entity.Property(e => e.Retencion).HasColumnName("retencion");
            entity.Property(e => e.TieneImpuesto).HasColumnName("tiene_impuesto");
            entity.Property(e => e.TipoImpuesto)
                .HasMaxLength(10)
                .HasColumnName("tipo_impuesto");
            entity.Property(e => e.TipoProducto)
                .HasMaxLength(10)
                .HasColumnName("tipo_producto");
            entity.Property(e => e.Traslado).HasColumnName("traslado");
        });

        modelBuilder.Entity<TblProductoSucursal>(entity =>
        {
            entity.HasKey(e => new { e.IdProducto, e.IdSucursal }).HasName("PK_tbl_producto_sucursal_1");

            entity.ToTable("tbl_producto_sucursal");

            entity.Property(e => e.IdProducto).HasColumnName("id_producto");
            entity.Property(e => e.IdSucursal).HasColumnName("id_sucursal");
            entity.Property(e => e.Eliminado).HasColumnName("eliminado");
            entity.Property(e => e.Precio)
                .HasColumnType("money")
                .HasColumnName("precio");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.TblProductoSucursals)
                .HasForeignKey(d => d.IdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbl_producto_sucursal_tbl_producto");

            entity.HasOne(d => d.IdSucursalNavigation).WithMany(p => p.TblProductoSucursals)
                .HasForeignKey(d => d.IdSucursal)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbl_producto_sucursal_tbl_sucursal");
        });

        modelBuilder.Entity<TblSucursal>(entity =>
        {
            entity.HasKey(e => e.IdSucursal);

            entity.ToTable("tbl_sucursal");

            entity.Property(e => e.IdSucursal).HasColumnName("id_sucursal");
            entity.Property(e => e.CalleDireccionSucursal)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("calle_direccion_sucursal");
            entity.Property(e => e.CentroCostoSucursal)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("centro_costo_sucursal");
            entity.Property(e => e.Eliminado).HasColumnName("eliminado");
            entity.Property(e => e.IdEmpresa).HasColumnName("Id_empresa");
            entity.Property(e => e.NombreSucursal)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre_sucursal");
            entity.Property(e => e.NumeroDireccionSucursal).HasColumnName("numero_direccion_sucursal");
            entity.Property(e => e.RazonSocialSucursal)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("razon_social_sucursal");
            entity.Property(e => e.RfcSucursal)
                .HasMaxLength(13)
                .HasColumnName("rfc_sucursal");

            entity.HasOne(d => d.IdEmpresaNavigation).WithMany(p => p.TblSucursals)
                .HasForeignKey(d => d.IdEmpresa)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbl_sucursal_tbl_empresa");
        });

        modelBuilder.Entity<TblTelefonoEmpresa>(entity =>
        {
            entity.HasKey(e => new { e.IdEmpresa, e.TelefonoEmpresa });

            entity.ToTable("tbl_telefono_empresa");

            entity.Property(e => e.IdEmpresa).HasColumnName("id_empresa");
            entity.Property(e => e.TelefonoEmpresa)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("telefono_empresa");

            entity.HasOne(d => d.IdEmpresaNavigation).WithMany(p => p.TblTelefonoEmpresas)
                .HasForeignKey(d => d.IdEmpresa)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbl_telefono_empresa_tbl_empresa");
        });

        modelBuilder.Entity<TblTelefonoSucursal>(entity =>
        {
            entity.HasKey(e => new { e.IdSucursal, e.TelefonoSucursal });

            entity.ToTable("tbl_telefono_sucursal");

            entity.Property(e => e.IdSucursal).HasColumnName("id_sucursal");
            entity.Property(e => e.TelefonoSucursal)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("telefono_sucursal");

            entity.HasOne(d => d.IdSucursalNavigation).WithMany(p => p.TblTelefonoSucursals)
                .HasForeignKey(d => d.IdSucursal)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbl_telefono_sucursal_tbl_sucursal");
        });

        modelBuilder.Entity<TblVentaDetalle>(entity =>
        {
            entity.HasKey(e => e.IdVentaDetalle);

            entity.ToTable("tbl_venta_detalle");

            entity.Property(e => e.IdVentaDetalle).HasColumnName("id_venta_detalle");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.Eliminado).HasColumnName("eliminado");
            entity.Property(e => e.IdProducto).HasColumnName("id_producto");
            entity.Property(e => e.IdVenta).HasColumnName("id_venta");
            entity.Property(e => e.PrecioUnitario)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("precio_unitario");
            entity.Property(e => e.Subtotal)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("subtotal");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.TblVentaDetalles)
                .HasForeignKey(d => d.IdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbl_venta_detalle_tbl_producto");

            entity.HasOne(d => d.IdVentaNavigation).WithMany(p => p.TblVentaDetalles)
                .HasForeignKey(d => d.IdVenta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbl_venta_detalle_tbl_venta");
        });

        modelBuilder.Entity<TblVentum>(entity =>
        {
            entity.HasKey(e => e.IdVenta);

            entity.ToTable("tbl_venta");

            entity.Property(e => e.IdVenta).HasColumnName("id_venta");
            entity.Property(e => e.Eliminado).HasColumnName("eliminado");
            entity.Property(e => e.FechaVenta)
                .HasColumnType("datetime")
                .HasColumnName("fecha_venta");
            entity.Property(e => e.IdCliente).HasColumnName("id_cliente");
            entity.Property(e => e.IdEmpleado).HasColumnName("id_empleado");
            entity.Property(e => e.TipoVenta)
                .HasMaxLength(10)
                .HasColumnName("tipo_venta");
            entity.Property(e => e.TotalVenta)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("total_venta");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.TblVenta)
                .HasForeignKey(d => d.IdCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbl_venta_tbl_cliente");

            entity.HasOne(d => d.IdEmpleadoNavigation).WithMany(p => p.TblVenta)
                .HasForeignKey(d => d.IdEmpleado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbl_venta_tbl_empleado");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
