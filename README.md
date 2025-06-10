# Faturacim ASP.NET API

Faturacim, .NET 8 kullanılarak gelistirilmiş örnek bir fatura yönetim sistemidir. Proje temel olarak kullanıcı doğrulama (JWT) ve fatura işlemlerini gerçekleştiren REST tabanlı bir web API içerir. Katmanlı mimari sayesinde kod farklı sorumluluklara ayrılmıştır.

## İçerik
- [Proje Yapısı](#proje-yapisi)
- [Kullanılan Teknolojiler](#kullanilan-teknolojiler)
- [Kurulum](#kurulum)
- [Veritabanı Migrasyonları](#veritabani-migrasyonlari)
- [Projeyi Çalıştırma](#projeyi-calistirma)
- [API Kullanımı](#api-kullanimi)
- [Katkıda Bulunma](#katkida-bulunma)

## Proje Yapısı

```
├── faturacim.Api            # API giriş noktası
├── faturacim.Application    # İş kuralları katmanı
├── faturacim.Domain         # Domain modelleri ve interface'ler
├── faturacim.Infrastructure # EF Core, repository ve güvenlik
└── faturacim.sln            # Çözüm dosyası
```

- **faturacim.Api**: Controller'ların bulunduğu, Swagger ve JWT ayarlarının yapıldığı proje.
- **faturacim.Application**: DTO sınıfları, servisler ve arayüzler.
- **faturacim.Domain**: Entity tanımları ve repository arayüzleri.
- **faturacim.Infrastructure**: Entity Framework Core bağlamı, repository implementasyonları ve JWT token üreticisi.

## Kullanılan Teknolojiler

- **.NET 8** & ASP.NET Core Web API
- **Entity Framework Core** (SQL Server)
- **JWT** tabanlı kimlik doğrulama
- **BCrypt.Net** ile parola şifreleme
- **Swagger** ile API dokümantasyonu

## Kurulum

1. Öncelikle [.NET 8 SDK](https://dotnet.microsoft.com/download) yüklü olmalıdır.
2. Depoyu klonladıktan sonra proje kök dizininde bağımlılıkları geri yükleyin:

   ```bash
   dotnet restore
   ```

3. `faturacim.Api/appsettings.json` dosyasında **DefaultConnection** için kendi SQL Server bağlantınızı belirtin:

   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=localhost;Database=Faturacim;Trusted_Connection=True;TrustServerCertificate=True"
   }
   ```

## Veritabanı Migrasyonları

Veritabanı ilk kez oluşturulacaksa EF Core migrasyonlarını çalıştırmak gerekir. Bunun için `faturacim.Infrastructure` projesi üzerinde şu komutu kullanın:

```bash
dotnet ef database update --project faturacim.Infrastructure
```

Bu komut var olan tüm migrasyonları çalıştırarak SQL Server'da gerekli tabloları oluşturur.

## Projeyi Çalıştırma

API'yi ayağa kaldırmak için:

```bash
dotnet run --project faturacim.Api
```

Uygulama varsayılan olarak `http://0.0.0.0:5202` adresinde çalışır. Geliştirme ortamında Swagger arayüzüne `http://localhost:5202/swagger` üzerinden erişebilirsiniz.

## API Kullanımı

### Kimlik Doğrulama

- **Kayıt**: `POST /api/auth/register`
  - İstek Gövdesi: `{"fullName": "Test", "email": "test@example.com", "password": "secret"}`
- **Giriş**: `POST /api/auth/login`
  - İstek Gövdesi: `{"email": "test@example.com", "password": "secret"}`
  - Yanıt başarılı olursa JWT token döner.

### Fatura İşlemleri
Tüm istekler `api/invoice` altında toplanır.

- `GET /api/invoice/user/{email}?startDate=2024-01-01&endDate=2024-12-31`
  - Belirli bir kullanıcıya ait faturaları (opsiyonel tarih aralığı filtreleriyle) getirir.
- `GET /api/invoice/{id}`
  - İlgili fatura kaydını ID ile döner.
- `POST /api/invoice`
  - Yeni fatura eklemek için kullanılır. İstek gövdesinde `Invoice` JSON nesnesi beklenir.

### Yetkilendirme

Fatura işlemleri için JWT token'ınızı `Authorization: Bearer {token}` başlığıyla iletmeniz gerekir.

## Katkıda Bulunma

1. Fork'layın ve yeni bir dal oluşturun.
2. Değişikliklerinizi commit'leyin ve test edin.
3. Pull Request göndererek katkıda bulunun.

Bu proje basit bir örnek olduğundan katkılarınızı bekliyoruz.

