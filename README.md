# 🌍 Vitour – Yapay Zeka Destekli Tur Rezervasyon ve Yönetim Sistemi | Bootcamp Case #3

<br>

Bu proje, **M&Y Yazılım Eğitim Akademi .NET Full Stack Bootcamp** kapsamında, **Murat Yücedağ** rehberliğinde geliştirilen **Case #3** çalışmasıdır. 

Vitour, modern bir seyahat acentesinin tüm operasyonlarını yönetebilmesi için geliştirilmiş, **yapay zeka destekli** uçtan uca bir web uygulamasıdır. Projede **ASP.NET Core MVC** ve **MongoDB** altyapısı kullanılmış; özellikle admin panelinin tasarımı ve harita görselleri tamamen AI (Claude & Gemini) ile üretilerek sisteme entegre edilmiştir.

---
## 🎯 Projenin Amacı

Bu proje ile aşağıdaki konularda derinlemesine pratik yapılmış ve yetkinlik kazanılmıştır:
* **NoSQL** mantığını kavrama ve **MongoDB** ile CRUD işlemleri gerçekleştirme.
* MVC mimarisinde **View Component** ve **Razor Pages** yeteneklerini maksimum seviyede kullanarak modüler arayüzler geliştirme.
* Yazılım geliştirme süreçlerinde **Yapay Zeka (AI)** araçlarını aktif kullanarak UI/UX tasarımını ve kodlama hızını optimize etme.
* DTO ve AutoMapper kullanarak veri transfer süreçlerini yönetme.
* Üçüncü parti raporlama kütüphaneleri (iText, ClosedXML) ile dinamik belgeler oluşturma.

---

## 🚀 Temel Özellikler

### 👤 Kullanıcı Arayüzü (Vitrin)
* **Modüler Yapı (View Components):** Sayfa içindeki bağımsız parçaların (footer, navbar, listeler vb.) View Component mimarisi ile dinamik ve modüler şekilde yönetilmesi.
* **Dinamik Listeleme ve Filtreleme:** MongoDB'den çekilen verilerle anlık kategori, tur tipi ve süre filtrelemesi. Dinamik sayfalama (Pagination) altyapısı.
* **Gelişmiş Tur Detay Sayfası (5 Sekmeli Yapı):**
  * **Açıklama & Program:** Gün gün detaylandırılmış tur akışları.
  * **AI Rota Haritaları:** Google Maps yerine, **Gemini AI** kullanılarak projeye özel üretilmiş konsept rota görselleri.
  * **Dinamik Yorum ve Puanlama:** Kullanıcıların 4 farklı kritere göre verdiği puanların matematiksel ortalamasının hesaplanması.
* **Akıllı Rezervasyon Akışı:** Kapasite kontrolü yapan, kontenjan dolduğunda otomatik engelleme sağlayan güvenli rezervasyon sistemi.
* **Çoklu Dil Desteği:** Cookie tabanlı Localization altyapısı ile Türkçe - İngilizce dil seçeneği.

### 🛡️ Admin Paneli (Yönetim)
* **Yapay Zeka Tasarımlı Arayüz:** Sıfırdan **Claude AI** kullanılarak prompt engineering ile tasarlanmış, modern ve kullanıcı dostu yönetim paneli.
* **Canlı Önizlemeli CRUD İşlemleri:** Tur eklerken veya düzenlerken, içeriğin vitrinde nasıl görüneceğini anlık olarak gösteren interaktif Razor View tasarımı.
* **Dinamik Tur Rozetleri:** Turlara "Popüler, Fırsat, Yeni" gibi etiketlerin admin panelinden atanabilmesi.
* **Dashboard ve KPI Metrikleri:** Aylık hedefler, toplam aktif tur sayısı ve hasılat verilerinin istatistiksel sunumu.

### 📊 Raporlama Sistemi
* **Gelişmiş PDF Çıktıları:** **iText 9** kütüphanesi ve özel font entegrasyonu ile **Türkçe karakter sorunu tamamen çözülmüş** dinamik PDF raporları.
* **Excel Dışa Aktarım:** **ClosedXML** ile tüm koleksiyonlar (Turlar, Yorumlar, Rezervasyonlar) için detaylı Excel çıktıları.

---

## 🛠 Kullanılan Teknolojiler

**Backend & Temel Yapı**
* ASP.NET Core MVC 8.0
* Razor Views & View Components
* Asenkron Programlama (async/await)

**Veritabanı**
* MongoDB (NoSQL)
* MongoDB.Driver

**Yapay Zeka & Tasarım (AI-Driven)**
* **Claude AI:** Admin paneli UI/UX tasarımı ve rezervasyon arayüzü kodlaması.
* **Gemini AI:** Tur rotaları için özelleştirilmiş harita görselleri üretimi.

**Araçlar ve Kütüphaneler**
* **AutoMapper:** DTO (Data Transfer Object) dönüşümleri için.
* **iText 9:** Dinamik PDF raporlama için.
* **ClosedXML:** Excel veri manipülasyonu için.

---

## 📸 Proje Ekran Görüntüleri

Tüm projenin ekran görüntülerini detaylı incelemek için aşağıdaki başlıklara tıklayabilirsiniz:

<details>
<summary><b>✨ 1. Vitrin ve Ana Sayfa Tasarımı</b></summary>
<br>

*View Component mimarisiyle oluşturulmuş dinamik vitrin ekranları.*

![Vitrin Ana Sayfa](GÖRSEL LİNKİ)
![Filtreleme ve Sayfalama](GÖRSEL LİNKİ)
![Footer ve İletişim](GÖRSEL LİNKİ)

</details>

<details>
<summary><b>🗺️ 2. Tur Detayları ve Yapay Zeka Haritaları</b></summary>
<br>

*5 sekmeli dinamik yapı, Gemini AI harita entegrasyonu ve yorum/puanlama sistemi.*

![Tur Açıklaması](GÖRSEL LİNKİ)
![Günlük Tur Planı](GÖRSEL LİNKİ)
![AI Rota Haritası](GÖRSEL LİNKİ)
![Dinamik Yorumlar ve Puanlar](GÖRSEL LİNKİ)

</details>

<details>
<summary><b>💳 3. Modern Rezervasyon Akışı</b></summary>
<br>

*Kapasite kontrollü rezervasyon adımları.*

![Rezervasyon Formu](GÖRSEL LİNKİ)
![Başarılı İşlem Ekranı](GÖRSEL LİNKİ)
![Kapasite Dolu Uyarısı](GÖRSEL LİNKİ)

</details>

<details>
<summary><b>🛡️ 4. Yapay Zeka Tasarımlı Admin Paneli</b></summary>
<br>

*Claude AI ile tasarlanan, KPI kartları ve Canlı Önizlemeli içerik yönetim ekranları.*

![Admin Dashboard](GÖRSEL LİNKİ)
![Tur Yönetimi](GÖRSEL LİNKİ)
![Canlı Önizlemeli Tur Ekleme](GÖRSEL LİNKİ)
![Yorum Yönetimi](GÖRSEL LİNKİ)

</details>

<details>
<summary><b>📊 5. Excel ve PDF Rapor Çıktıları</b></summary>
<br>

*Sistem üzerinden oluşturulan Türkçe karakter destekli profesyonel çıktılar.*

![Raporlar Sayfası](GÖRSEL LİNKİ)
![PDF Çıktısı](GÖRSEL LİNKİ)
![Excel Çıktısı](GÖRSEL LİNKİ)

</details>

---

## 🎓 Eğitim

Bu proje, **M&Y Yazılım Eğitim Akademi** tarafından verilen **.NET Full Stack Bootcamp** kapsamında geliştirilen Case #3 çalışmasıdır. Bu süreçteki katkılarından dolayı **Murat Yücedağ** hocama teşekkür ederim.

---

## 👩‍💻 Developer

**Sena Nur Özdemir** GitHub: [https://github.com/sena-nur-ozdemir](https://github.com/sena-nur-ozdemir)
