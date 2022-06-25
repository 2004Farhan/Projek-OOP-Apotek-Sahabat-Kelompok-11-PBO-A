--
-- PostgreSQL database 
--

SET statement_timeout = 0;
SET lock_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SET check_function_bodies = false;
SET client_min_messages = warning;



SET default_tablespace = '';

SET default_with_oids = false;


---
--- drop tables
---


DROP TABLE IF EXISTS jenisObat;
DROP TABLE IF EXISTS supplier;
DROP TABLE IF EXISTS historyHargaObat;
DROP TABLE IF EXISTS obat;
DROP TABLE IF EXISTS customer;
DROP TABLE IF EXISTS karyawan;
DROP TABLE IF EXISTS jenisTransaksi;
DROP TABLE IF EXISTS transaksi;
DROP TABLE IF EXISTS detailTransaksi;


--
-- Name: jenisObat; Type: TABLE; Schema: public; Owner: -; Tablespace: 
--

CREATE TABLE jenisObat (
    id_jenis_obat smallint primary key NOT NULL,
    jenis_obat character varying(30) NOT NULL,
    keterangan text
);


--
-- Name: supplier; Type: TABLE; Schema: public; Owner: -; Tablespace: 
--

CREATE TABLE supplier (
    id_supplier smallint primary key NOT NULL,
    nama_perusahaan character varying(30) NOT NULL,
    no_telp character varying(20) NOT NULL,
    alamat character varying(30) NOT NULL,
    kota character varying(20) NOT NULL,
    provinsi character varying(30) NOT NULL,
    negara character varying(30) NOT NULL
);


--
-- Name: obat; Type: TABLE; Schema: public; Owner: -; Tablespace: 
--

CREATE TABLE obat (
    id_obat smallint primary key NOT NULL,
	id_jenis_obat smallint NOT NULL,
	id_supplier smallint NOT NULL,
    nama_obat character varying(30) NOT NULL,
    kuantitas_per_unit smallint NOT NULL,
    harga integer NOT NULL,
    stok integer NOT NULL,
    discontinue integer NOT NULL
);

ALTER TABLE obat
	ADD CONSTRAINT fk_jenis_obat FOREIGN KEY(id_jenis_obat) 
	REFERENCES jenisObat(id_jenis_obat);
ALTER TABLE obat
	ADD CONSTRAINT fk_supplier FOREIGN KEY(id_supplier) 
	REFERENCES supplier(id_supplier);

--
-- Name: historyHargaObat; Type: TABLE; Schema: public; Owner: -; Tablespace: 
--

CREATE TABLE historyHargaObat (
    id_history_harga smallint primary key NOT NULL,
	id_obat smallint NOT NULL,
    harga character varying(40) NOT NULL,
    mulai_berlaku date NOT NULL,
    akhir_berlaku date,
	status character varying(5) NOT NULL
);

ALTER TABLE historyHargaObat
	ADD CONSTRAINT fk_obat FOREIGN KEY(id_obat) 
	REFERENCES obat(id_obat);


--
-- Name: customer; Type: TABLE; Schema: public; Owner: -; Tablespace: 
--

CREATE TABLE customer (
    id_customer smallint primary key NOT NULL,
    nama_customer character varying(35) NOT NULL
);


--
-- Name: karyawan; Type: TABLE; Schema: public; Owner: -; Tablespace: 
--

CREATE TABLE karyawan (
    id_karyawan smallint primary key NOT NULL,
    nama_karyawan character varying(30) NOT NULL,
    jabatan character varying(25) NOT NULL,
    tanggal_lahir date NOT NULL,
    tanggal_masuk date NOT NULL,
	no_telp character varying(20) NOT NULL,
    alamat character varying(30) NOT NULL,
    kota character varying(30) NOT NULL,
    provinsi character varying(30) NOT NULL,
	username character varying(20) NOT NULL,
	password character varying(20) NOT NULL
);


--
-- Name: jenisTransaksi; Type: TABLE; Schema: public; Owner: -; Tablespace: 
--

CREATE TABLE jenisTransaksi (
    id_jenis_transaksi smallint primary key NOT NULL,
    jenis_transaksi character varying(30) NOT NULL,
	keterangan text
);


--
-- Name: transaksi; Type: TABLE; Schema: public; Owner: -; Tablespace: 
--

CREATE TABLE transaksi (
    id_transaksi smallint primary key NOT NULL,
    tanggal timestamp default now() NOT NULL,
	id_jenis_transaksi smallint NOT NULL,
	id_karyawan smallint NOT NULL,
	id_customer smallint NOT NULL
);

ALTER TABLE transaksi
	ADD CONSTRAINT fk_jenis_transaksi FOREIGN KEY(id_jenis_transaksi) 
	REFERENCES jenisTransaksi(id_jenis_transaksi);
ALTER TABLE transaksi
	ADD CONSTRAINT fk_karyawan FOREIGN KEY(id_karyawan) 
	REFERENCES karyawan(id_karyawan);
ALTER TABLE transaksi
	ADD CONSTRAINT fk_customer FOREIGN KEY(id_customer) 
	REFERENCES customer(id_customer);


--
-- Name: detailTransaksi; Type: TABLE; Schema: public; Owner: -; Tablespace: 
--

CREATE TABLE detailTransaksi (
    id_detail_transaksi smallint primary key NOT NULL,
	id_transaksi smallint NOT NULL,
	id_obat smallint NOT NULL,
    harga_jual integer NOT NULL,
    kuantitas smallint NOT NULL,
    diskon integer NOT NULL
);

ALTER TABLE detailTransaksi
	ADD CONSTRAINT fk_transaksi FOREIGN KEY(id_transaksi) 
	REFERENCES transaksi(id_transaksi);
ALTER TABLE detailTransaksi
	ADD CONSTRAINT fk_obat FOREIGN KEY(id_obat) 
	REFERENCES obat(id_obat);


--
-- Data for Name: jenisObat; Type: TABLE DATA; Schema: public; Owner: -
--

INSERT INTO jenisObat VALUES (1, 'Obat Cair', 'Zat aktif yang dilarutkan dalam cairan');
INSERT INTO jenisObat VALUES (2, 'Tablet', 'zat aktif yang dikombinasikan dengan bahan-bahan tertentu dan kemudian dipadatkan');
INSERT INTO jenisObat VALUES (3, 'Kapsul', 'bubuk tersimpan di dalam tabung plastik kecil yang bisa larut secara perlahan.');
INSERT INTO jenisObat VALUES (4, 'Obat Oles', 'obat topikal atau obat luar karena diaplikasikan langsung di atas kulit');
INSERT INTO jenisObat VALUES (5, 'Inhaler', 'terdapat dalam bentuk tabung yang berisi zat aktif');
INSERT INTO jenisObat VALUES (6, 'Obat Tetes', 'diaplikasikan langsung pada bagian tubuh');


--
-- Data for Name: supplier; Type: TABLE DATA; Schema: public; Owner: -
--

INSERT INTO supplier VALUES (1, 'PT. PIM PHARMACEUTICALS', '(0343)631294', 'Jawi, Candi Wates, Prigen', 'Pasuruan', 'Jawa Timur', 'Indonesia');
INSERT INTO supplier VALUES (2, 'NOVAPHARIN', '(031)7994614', 'Menganti', 'Gresik', 'Jawa Timur', 'Indonesia');


--
-- Data for Name: obat; Type: TABLE DATA; Schema: public; Owner: -
--

INSERT INTO obat VALUES (1, 2, 1, 'Paracetamol', 10, 3500, 12, 0);
INSERT INTO obat VALUES (2, 2, 2, 'Ibuprofen', 10, 4400, 15, 0);


--
-- Data for Name: historyHargaObat; Type: TABLE DATA; Schema: public; Owner: -
--

INSERT INTO historyHargaObat VALUES (1, 1, 3300, '2022-01-20', '2022-02-25', 'N');
INSERT INTO historyHargaObat VALUES (2, 1, 3500, '2022-02-25', null, 'Y');
INSERT INTO historyHargaObat VALUES (3, 2, 4400, '2022-03-20', null, 'Y');


--
-- Data for Name: customer; Type: TABLE DATA; Schema: public; Owner: -
--

INSERT INTO customer VALUES (1, 'Dina');
INSERT INTO customer VALUES (2, 'Firman');
INSERT INTO customer VALUES (3, 'Bayyin');


--
-- Data for Name: karyawan; Type: TABLE DATA; Schema: public; Owner: -
--

INSERT INTO karyawan VALUES (1, 'Farhan', 'Apoteker', '1999-12-4', '2022-02-23', '081213356789', 'Jalan Salak', 'Jember', 'Jawa Timur', 'firman', 'firman123');
INSERT INTO karyawan VALUES (2, 'Bayyintaro', 'Apoteker', '1999-09-29', '2021-12-01', '085213520012', 'Jalan Jambu', 'Jember', 'Jawa Timur', 'usrbayyin', 'bayy456');


--
-- Data for Name: jenisTransaksi; Type: TABLE DATA; Schema: public; Owner: -
--

INSERT INTO jenisTransaksi VALUES (1, 'Resep Dokter');
INSERT INTO jenisTransaksi VALUES (2, 'Umum', 'Obat yang dijual harus diseleksi terlebih dahulu');


--
-- Data for Name: transaksi; Type: TABLE DATA; Schema: public; Owner: -
--

INSERT INTO transaksi (id_transaksi, id_jenis_transaksi, id_karyawan, id_customer) VALUES (1, 2, 2, 3);
INSERT INTO transaksi (id_transaksi, id_jenis_transaksi, id_karyawan, id_customer) VALUES (2, 1, 2, 2);


--
-- Data for Name: detailTransaksi; Type: TABLE DATA; Schema: public; Owner: -
--

INSERT INTO detailTransaksi VALUES (1, 2, 1, 3500, 2, 0);
INSERT INTO detailTransaksi VALUES (2, 1, 2, 4400, 1, 0);


--
-- PostgreSQL database complete
--

