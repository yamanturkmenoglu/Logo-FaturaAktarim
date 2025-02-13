-- 1. HRK.FaturaBasliklari Tablosu
CREATE TABLE [HRK.FaturaBasliklari] (
    FaturaId INT IDENTITY(1,1) PRIMARY KEY,
    TarihSaat DATETIME NULL,
    CariKodu NVARCHAR(50) NOT NULL,
    CariUnvan NVARCHAR(100) NOT NULL,
    FaturaNumarasi NVARCHAR(50) NOT NULL,
    ToplamIndirim DECIMAL(18,4) NOT NULL,
	BayiKarKDV DECIMAL(18,4),
	FirmaKarKDV DECIMAL(18,4),
	ToplamTutar DECIMAL(18,4) NOT NULL,
	iptal NVARCHAR(50) NOT NULL,
);

-- 2. HRK.FaturaKalemleri Tablosu
CREATE TABLE [HRK.FaturaKalemleri] (
    KalemId INT IDENTITY(1,1) PRIMARY KEY,
    FaturaId INT NOT NULL FOREIGN KEY REFERENCES [HRK.FaturaBasliklari](FaturaId),
    UrunKodu NVARCHAR(50) NOT NULL,
    UrunAdi NVARCHAR(100) NOT NULL,
    Miktar DECIMAL(18,0) NOT NULL,
    BirimFiyat DECIMAL(18,4),
    KDVOrani INT NOT NULL,
    KDVliBirimFiyat DECIMAL(18,4) NOT NULL,
    ToplamTutar DECIMAL(18,4) NOT NULL
);



-- 3. TNM.MALZEME FÝYAT Tablosu
CREATE TABLE [TNM.MALZEME FÝYAT] (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    CODE NVARCHAR(50) NOT NULL,       
    NAME NVARCHAR(255) NOT NULL,      
    BAYI_SATIS_FIYATI DECIMAL(18, 4)  
);
