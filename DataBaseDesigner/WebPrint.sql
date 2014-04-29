-- =============================================================================
-- Diagram Name: WebPrintStandard
-- Created on: 2014/4/28 17:26:25
-- Diagram Version: 2.9
-- =============================================================================

DROP TABLE IF EXISTS "wp_user" CASCADE;

CREATE TABLE "wp_user" (
	"id" SERIAL NOT NULL,
	"print_shop_id" int4 NOT NULL DEFAULT 0,
	"user_name" varchar(50) NOT NULL DEFAULT '',
	"password" varchar(50) NOT NULL DEFAULT '',
	"display_name" varchar(50) NOT NULL DEFAULT '',
	"email" varchar(255) NOT NULL DEFAULT '',
	"vendor_code" varchar(50) NOT NULL DEFAULT '',
	"active" int2 NOT NULL DEFAULT 0,
	"created_time" timestamp NOT NULL DEFAULT now(),
	"modified_time" timestamp NOT NULL DEFAULT now(),
	CONSTRAINT "wp_user_id_pkey" PRIMARY KEY("id"),
	CONSTRAINT "wp_user_name_key" UNIQUE("user_name")
);

COMMENT ON TABLE "wp_user" IS '用户信息表';

COMMENT ON COLUMN "wp_user"."id" IS '自增长Id';

COMMENT ON COLUMN "wp_user"."print_shop_id" IS '生产工厂标识, 客户下单到不同工厂生产';

COMMENT ON COLUMN "wp_user"."user_name" IS '用户登录名称';

COMMENT ON COLUMN "wp_user"."password" IS '登录密码';

COMMENT ON COLUMN "wp_user"."display_name" IS '展示名称, 如供应商名称';

COMMENT ON COLUMN "wp_user"."email" IS '邮件列表, 可以多个邮件地址, | 分开';

COMMENT ON COLUMN "wp_user"."vendor_code" IS '订单供应商标识 vendor code';

COMMENT ON COLUMN "wp_user"."active" IS '标识用户是否可用 0是 1否';

COMMENT ON COLUMN "wp_user"."created_time" IS '创建时间';

COMMENT ON COLUMN "wp_user"."modified_time" IS '最后修改时间';

DROP TABLE IF EXISTS "wp_group" CASCADE;

CREATE TABLE "wp_group" (
	"id" SERIAL NOT NULL,
	"name" varchar(50) NOT NULL DEFAULT '',
	"display_name" varchar(50) NOT NULL DEFAULT '',
	"created_time" timestamp NOT NULL DEFAULT now(),
	CONSTRAINT "wp_group_pkey" PRIMARY KEY("id"),
	CONSTRAINT "wp_group_name_key" UNIQUE("name")
);

COMMENT ON TABLE "wp_group" IS '用户组';

COMMENT ON COLUMN "wp_group"."id" IS '自增长Id';

COMMENT ON COLUMN "wp_group"."name" IS '组名';

COMMENT ON COLUMN "wp_group"."display_name" IS '展示名';

COMMENT ON COLUMN "wp_group"."created_time" IS '创建时间';

DROP TABLE IF EXISTS "wp_permission" CASCADE;

CREATE TABLE "wp_permission" (
	"id" SERIAL NOT NULL,
	"name" varchar(50) NOT NULL DEFAULT '',
	"display_name" varchar(50) NOT NULL DEFAULT '',
	"created_time" timestamp NOT NULL DEFAULT now(),
	CONSTRAINT "wp_permission_pkey" PRIMARY KEY("id"),
	CONSTRAINT "wp_permission_name_key" UNIQUE("name")
);

COMMENT ON TABLE "wp_permission" IS '权限表';

COMMENT ON COLUMN "wp_permission"."id" IS '自增长Id';

COMMENT ON COLUMN "wp_permission"."name" IS '权限名';

COMMENT ON COLUMN "wp_permission"."display_name" IS '展示名';

COMMENT ON COLUMN "wp_permission"."created_time" IS '创建时间';

DROP TABLE IF EXISTS "wp_group_permission" CASCADE;

CREATE TABLE "wp_group_permission" (
	"id" SERIAL NOT NULL,
	"group_id" int4 NOT NULL DEFAULT 0,
	"permission_id" int4 NOT NULL DEFAULT 0,
	CONSTRAINT "wp_group_permission_pkey" PRIMARY KEY("id"),
	CONSTRAINT "wp_group_permission_group_id_permission_id_key" UNIQUE("group_id","permission_id")
);

COMMENT ON TABLE "wp_group_permission" IS '用户组与权限的关系表';

COMMENT ON COLUMN "wp_group_permission"."id" IS '自增长Id';

COMMENT ON CONSTRAINT "wp_group_permission_pkey" ON "wp_group_permission" IS 'False';

COMMENT ON CONSTRAINT "wp_group_permission_group_id_permission_id_key" ON "wp_group_permission" IS 'False';

DROP TABLE IF EXISTS "wp_user_group" CASCADE;

CREATE TABLE "wp_user_group" (
	"id" SERIAL NOT NULL,
	"user_id" int4 NOT NULL DEFAULT 0,
	"group_id" int4 NOT NULL DEFAULT 0,
	CONSTRAINT "wp_user_group_id_pkey" PRIMARY KEY("id")
);

COMMENT ON TABLE "wp_user_group" IS '用户与用户组的关系表';

COMMENT ON COLUMN "wp_user_group"."id" IS '自增长Id';

DROP TABLE IF EXISTS "wp_order" CASCADE;

CREATE TABLE "wp_order" (
	"id" SERIAL NOT NULL,
	"print_shop_id" int4 NOT NULL DEFAULT 0,
	"order_ship_bill_id" int4 NOT NULL DEFAULT 0,
	"associated_order_id" int4 NOT NULL DEFAULT 0,
	"job_no" varchar(50) NOT NULL DEFAULT '',
	"po_no" varchar(50) NOT NULL DEFAULT '',
	"type" varchar(50) NOT NULL DEFAULT '',
	"vendor_code" varchar(50) NOT NULL DEFAULT '',
	"remark" varchar(50) NOT NULL DEFAULT '',
	"wastage" varchar(50) NOT NULL DEFAULT '',
	"production" int2 NOT NULL DEFAULT 0,
	"completion_date" varchar(50) NOT NULL DEFAULT '',
	"status" varchar(50) NOT NULL DEFAULT '',
	"so_no" varchar(50) NOT NULL DEFAULT '',
	"active" int2 NOT NULL DEFAULT 0,
	"created_id" int4 NOT NULL DEFAULT 0,
	"created_time" timestamp NOT NULL DEFAULT now(),
	"modified_id" int4 NOT NULL DEFAULT 0,
	"modified_time" timestamp NOT NULL DEFAULT now(),
	"file_name" varchar(50) NOT NULL DEFAULT '',
	CONSTRAINT "wp_order_id_pkey" PRIMARY KEY("id"),
	CONSTRAINT "wp_order_job_number_key" UNIQUE("job_no"),
	CONSTRAINT "wp_order_order_ship_bill_id_key" UNIQUE("order_ship_bill_id")
);

COMMENT ON TABLE "wp_order" IS '订单表';

COMMENT ON COLUMN "wp_order"."id" IS '订单自增长Id';

COMMENT ON COLUMN "wp_order"."print_shop_id" IS '生产工厂编号';

COMMENT ON COLUMN "wp_order"."order_ship_bill_id" IS '关联订单号 如RFID订单同时Graphic Order 如Wacoal';

COMMENT ON COLUMN "wp_order"."associated_order_id" IS '关联订单号 如RFID订单同时Graphic Order 如Wacoal';

COMMENT ON COLUMN "wp_order"."job_no" IS '生产任务号';

COMMENT ON COLUMN "wp_order"."po_no" IS '订单编号';

COMMENT ON COLUMN "wp_order"."type" IS '订单类型';

COMMENT ON COLUMN "wp_order"."vendor_code" IS '供应商编码';

COMMENT ON COLUMN "wp_order"."remark" IS '描述订单 如类型';

COMMENT ON COLUMN "wp_order"."wastage" IS '损耗比例';

COMMENT ON COLUMN "wp_order"."production" IS '是否生产EPC 开始生产';

COMMENT ON COLUMN "wp_order"."status" IS '状态 如Printing';

COMMENT ON COLUMN "wp_order"."so_no" IS '如rpac so';

COMMENT ON COLUMN "wp_order"."active" IS '是否删除 0否 1是';

COMMENT ON COLUMN "wp_order"."created_id" IS '创建人';

COMMENT ON COLUMN "wp_order"."created_time" IS '创建时间';

COMMENT ON COLUMN "wp_order"."modified_id" IS '修改该人';

COMMENT ON COLUMN "wp_order"."modified_time" IS '最后修改时间';

COMMENT ON COLUMN "wp_order"."file_name" IS '来自订单文件名';

DROP TABLE IF EXISTS "wp_print_shop" CASCADE;

CREATE TABLE "wp_print_shop" (
	"id" SERIAL NOT NULL,
	"code" varchar(50) NOT NULL DEFAULT '',
	"display_name" varchar(50) NOT NULL DEFAULT '',
	"email_list" varchar(500) NOT NULL,
	CONSTRAINT "wp_print_shop_id_pkey" PRIMARY KEY("id"),
	CONSTRAINT "wp_print_shop_code_key" UNIQUE("code")
);

COMMENT ON TABLE "wp_print_shop" IS '生产工厂表';

COMMENT ON COLUMN "wp_print_shop"."id" IS '自增长Id';

COMMENT ON COLUMN "wp_print_shop"."code" IS '编号';

COMMENT ON COLUMN "wp_print_shop"."display_name" IS '展示名';

COMMENT ON COLUMN "wp_print_shop"."email_list" IS 'notification emials list';

DROP TABLE IF EXISTS "wp_order_detail" CASCADE;

CREATE TABLE "wp_order_detail" (
	"id" SERIAL NOT NULL,
	"order_id" int4 NOT NULL,
	"format_id" int4 NOT NULL,
	"original_qty" int4 NOT NULL DEFAULT 0,
	"qty" int4 NOT NULL DEFAULT 0,
	"spare_qty" int4 NOT NULL DEFAULT 0,
	"remain_qty" int4 NOT NULL DEFAULT 0,
	"epc_serial_begin" int8 NOT NULL DEFAULT 0,
	"epc_serial_end" int8 NOT NULL DEFAULT 0,
	"epc_code_begin" varchar(50) NOT NULL DEFAULT '',
	"epc_code_end" varchar(50) NOT NULL DEFAULT '',
	"print_begin_epc" varchar(50) NOT NULL DEFAULT '',
	"status" varchar(50) NOT NULL DEFAULT '',
	"line_no" int4 NOT NULL DEFAULT 1,
	"remark" varchar(50) NOT NULL DEFAULT '',
	"upc" varchar(50) NOT NULL DEFAULT '',
	"gtin" varchar(50) NOT NULL DEFAULT '',
	"po_no" varchar(50) NOT NULL DEFAULT '',
	"style" varchar(50) NOT NULL DEFAULT '',
	"style_desc" varchar(50) NOT NULL DEFAULT '',
	"color" varchar(50) NOT NULL DEFAULT '',
	"color_desc" varchar(50) NOT NULL DEFAULT '',
	"size" varchar(50) NOT NULL DEFAULT '',
	"size_desc" varchar(50) NOT NULL DEFAULT '',
	"vendor" varchar(50) NOT NULL DEFAULT '',
	"vendor_code" varchar(50) NOT NULL DEFAULT '',
	"logo" varchar(50) NOT NULL DEFAULT '',
	"unit" varchar(255) NOT NULL DEFAULT '',
	"price" varchar(50) NOT NULL DEFAULT '',
	"production_type" varchar(50) NOT NULL DEFAULT '',
	"desc" varchar(255) NOT NULL DEFAULT '',
	"graphic_category" varchar(50) NOT NULL DEFAULT '',
	"graphic_name" varchar(50) NOT NULL DEFAULT '',
	"graphic_code" varchar(50) NOT NULL DEFAULT '',
	"graphic_preview_image" varchar(50) NOT NULL DEFAULT '',
	"graphic_surcharge" varchar(50) NOT NULL DEFAULT '',
	"graphic_lead_time" varchar(255) NOT NULL DEFAULT '',
	"graphic_tier" varchar(255) NOT NULL DEFAULT '',
	"graphic_uom" varchar(255) NOT NULL DEFAULT '',
	"active" int2 NOT NULL DEFAULT 0,
	"created_id" int4 NOT NULL DEFAULT 0,
	"created_time" timestamp NOT NULL DEFAULT now(),
	"modified_id" int4 NOT NULL DEFAULT 0,
	"modified_time" timestamp NOT NULL DEFAULT now(),
	"file_name" varchar(50) NOT NULL DEFAULT '',
	"item_id" int4 NOT NULL DEFAULT 0,
	"graphic_tag_item_detail_id" int4 NOT NULL DEFAULT 0,
	CONSTRAINT "wp_order_detail_id_pkey" PRIMARY KEY("id")
);

COMMENT ON TABLE "wp_order_detail" IS '订单明细表';

COMMENT ON COLUMN "wp_order_detail"."id" IS '自增长Id';

COMMENT ON COLUMN "wp_order_detail"."order_id" IS '订单Id';

COMMENT ON COLUMN "wp_order_detail"."format_id" IS '关联的 label format';

COMMENT ON COLUMN "wp_order_detail"."original_qty" IS '原始数量 文件数据中数据';

COMMENT ON COLUMN "wp_order_detail"."qty" IS '处理 original qty 之后的数量, 如原始数量小于100则qty为100, 就是qty to ship';

COMMENT ON COLUMN "wp_order_detail"."spare_qty" IS '损耗数量';

COMMENT ON COLUMN "wp_order_detail"."remain_qty" IS '还剩打印数量 初始数量 remain_qty = qty + spare_qty';

COMMENT ON COLUMN "wp_order_detail"."epc_serial_begin" IS 'epc 开始序列号';

COMMENT ON COLUMN "wp_order_detail"."epc_serial_end" IS 'epc 结束序列号';

COMMENT ON COLUMN "wp_order_detail"."epc_code_begin" IS '开始epc';

COMMENT ON COLUMN "wp_order_detail"."epc_code_end" IS '结束epc';

COMMENT ON COLUMN "wp_order_detail"."print_begin_epc" IS '开始打印epc';

COMMENT ON COLUMN "wp_order_detail"."status" IS 'detail line 生产状态 如New';

COMMENT ON COLUMN "wp_order_detail"."line_no" IS 'wp_item关联 作参考用';

COMMENT ON COLUMN "wp_order_detail"."remark" IS '描述';

COMMENT ON COLUMN "wp_order_detail"."active" IS '表示是否可用 0可用 1不可用';

COMMENT ON COLUMN "wp_order_detail"."item_id" IS 'wp_item关联 作参考用';

COMMENT ON COLUMN "wp_order_detail"."graphic_tag_item_detail_id" IS '兼容graphic tag order';

DROP TABLE IF EXISTS "wp_format" CASCADE;

CREATE TABLE "wp_format" (
	"id" SERIAL NOT NULL,
	"category_id" int4 NOT NULL DEFAULT 0,
	"name" varchar(50) NOT NULL DEFAULT '',
	"display_name" varchar(50) NOT NULL DEFAULT '',
	"path" varchar(50) NOT NULL DEFAULT '',
	"preview_image" varchar(255) NOT NULL DEFAULT '',
	"code" varchar(50) NOT NULL DEFAULT '',
	"type" varchar(50) NOT NULL DEFAULT '',
	"created_id" int4 NOT NULL DEFAULT 0,
	"created_time" timestamp NOT NULL DEFAULT now(),
	"modified_id" int4 NOT NULL DEFAULT 0,
	"modified_time" timestamp NOT NULL DEFAULT now(),
	CONSTRAINT "wp_format_id_pkey" PRIMARY KEY("id")
);

COMMENT ON TABLE "wp_format" IS 'label format 表';

COMMENT ON COLUMN "wp_format"."id" IS '自增长Id';

COMMENT ON COLUMN "wp_format"."category_id" IS '保留';

COMMENT ON COLUMN "wp_format"."name" IS '实际文件名';

COMMENT ON COLUMN "wp_format"."display_name" IS '展示名';

COMMENT ON COLUMN "wp_format"."path" IS 'labelformat路径';

COMMENT ON COLUMN "wp_format"."preview_image" IS '预览图片路径';

COMMENT ON COLUMN "wp_format"."code" IS '配匹 标记';

COMMENT ON COLUMN "wp_format"."type" IS 'RFID or Non-RFID';

DROP TABLE IF EXISTS "wp_item" CASCADE;

CREATE TABLE "wp_item" (
	"id" SERIAL NOT NULL,
	"po_no" varchar(50) NOT NULL DEFAULT '',
	"style" varchar(50) NOT NULL DEFAULT '',
	"style_desc" varchar(50) NOT NULL DEFAULT '',
	"color" varchar(50) NOT NULL DEFAULT '',
	"color_desc" varchar(50) NOT NULL DEFAULT '',
	"size" varchar(50) NOT NULL DEFAULT '',
	"size_desc" varchar(50) NOT NULL DEFAULT '',
	"vendor" varchar(50) NOT NULL DEFAULT '',
	"vendor_code" varchar(50) NOT NULL DEFAULT '',
	"upc" varchar(50) NOT NULL DEFAULT '',
	"gtin" varchar(50) NOT NULL DEFAULT '',
	"price" varchar(50) NOT NULL DEFAULT '',
	"qty" int4 NOT NULL DEFAULT 0,
	"logo" varchar(50) NOT NULL DEFAULT '',
	"active" int2 NOT NULL DEFAULT 0,
	"type" varchar(50) NOT NULL DEFAULT '',
	"created_id" int4 NOT NULL,
	"created_time" timestamp NOT NULL DEFAULT now(),
	"modified_id" int4 NOT NULL,
	"modified_time" timestamp NOT NULL DEFAULT now(),
	"file_name" varchar(50) NOT NULL DEFAULT '',
	CONSTRAINT "wp_item_id_pkey" PRIMARY KEY("id")
);

COMMENT ON TABLE "wp_item" IS 'item master 表';

COMMENT ON COLUMN "wp_item"."id" IS '自增长Id';

COMMENT ON COLUMN "wp_item"."active" IS '0可用 1不可用';

COMMENT ON COLUMN "wp_item"."file_name" IS '来自文件的名称';

DROP TABLE IF EXISTS "wp_upc" CASCADE;

CREATE TABLE "wp_upc" (
	"id" SERIAL NOT NULL,
	"upc" varchar(50) NOT NULL DEFAULT '',
	"gtin" varchar(50) NOT NULL DEFAULT '',
	"last_serial_number" int8 NOT NULL DEFAULT 0,
	"active" int2 NOT NULL DEFAULT 0,
	"created_id" int4 NOT NULL,
	"created_time" timestamp NOT NULL DEFAULT now(),
	"modified_id" int4 NOT NULL,
	"modified_time" timestamp NOT NULL DEFAULT now(),
	CONSTRAINT "wp_upc_id_pkey" PRIMARY KEY("id"),
	CONSTRAINT "wp_upc_gtin_key" UNIQUE("gtin")
);

COMMENT ON TABLE "wp_upc" IS 'upc last_qty 信息';

COMMENT ON COLUMN "wp_upc"."id" IS '自增长Id';

COMMENT ON COLUMN "wp_upc"."gtin" IS 'upc 可能位数不一 gtin位数一定 必须唯一';

COMMENT ON COLUMN "wp_upc"."last_serial_number" IS 'gtin(upc) 最后一个已用序列号';

COMMENT ON COLUMN "wp_upc"."active" IS '0可用 1不可用';

DROP TABLE IF EXISTS "wp_print_history" CASCADE;

CREATE TABLE "wp_print_history" (
	"id" SERIAL NOT NULL,
	"order_detail_id" int4 NOT NULL,
	"qty" int4 NOT NULL DEFAULT 0,
	"epc_code_begin" varchar(50) NOT NULL DEFAULT '',
	"epc_code_end" varchar(50) NOT NULL DEFAULT '',
	"client_ip" varchar(50) NOT NULL DEFAULT '',
	"printer_name" varchar(50) NOT NULL DEFAULT '',
	"format_name" varchar(50) NOT NULL DEFAULT '',
	"upc" varchar(50) NOT NULL DEFAULT '',
	"message" varchar(500) NOT NULL DEFAULT '',
	"active" int2 NOT NULL DEFAULT 0,
	"type" varchar(50) NOT NULL DEFAULT '',
	"created_id" int4 NOT NULL,
	"created_time" timestamp NOT NULL DEFAULT now(),
	CONSTRAINT "wp_print_history_id_pkey" PRIMARY KEY("id")
);

COMMENT ON TABLE "wp_print_history" IS '打印历史记录';

COMMENT ON COLUMN "wp_print_history"."id" IS '自增长Id';

COMMENT ON COLUMN "wp_print_history"."order_detail_id" IS '对应order_detail id';

COMMENT ON COLUMN "wp_print_history"."active" IS '0可用 1不可用';

COMMENT ON COLUMN "wp_print_history"."type" IS '类型 print or reprint';

DROP TABLE IF EXISTS "wp_ship_bill" CASCADE;

CREATE TABLE "wp_ship_bill" (
	"id" SERIAL NOT NULL,
	"company" varchar(50) NOT NULL DEFAULT '',
	"attention" varchar(50) NOT NULL DEFAULT '',
	"address" varchar(50) NOT NULL DEFAULT '',
	"address2" varchar(50) NOT NULL DEFAULT '',
	"address3" varchar(50) NOT NULL DEFAULT '',
	"city_town" varchar(50) NOT NULL DEFAULT '',
	"state" varchar(50) NOT NULL DEFAULT '',
	"zip_code" varchar(50) NOT NULL DEFAULT '',
	"country" varchar(50) NOT NULL DEFAULT '',
	"phone" varchar(50) NOT NULL DEFAULT '',
	"fax" varchar(50) NOT NULL DEFAULT '',
	"email" varchar(255) NOT NULL DEFAULT '',
	"remark" varchar(500) NOT NULL DEFAULT '',
	"type" varchar(50) DEFAULT '',
	"active" int4 NOT NULL DEFAULT 0,
	"created_id" int4 NOT NULL,
	"created_time" timestamp NOT NULL DEFAULT now(),
	"modified_id" int4 NOT NULL,
	"modified_time" timestamp NOT NULL DEFAULT now(),
	CONSTRAINT "wp_ship_bill_id_pkey" PRIMARY KEY("id")
);

COMMENT ON TABLE "wp_ship_bill" IS 'ship bill address info';

COMMENT ON COLUMN "wp_ship_bill"."id" IS '自增长Id';

COMMENT ON COLUMN "wp_ship_bill"."type" IS 'Ship or Bill';

COMMENT ON COLUMN "wp_ship_bill"."active" IS '0可用 1不可用';

DROP TABLE IF EXISTS "wp_user_address_book" CASCADE;

CREATE TABLE "wp_user_address_book" (
	"id" SERIAL NOT NULL,
	"user_id" int4 NOT NULL,
	"ship_id" int4 NOT NULL,
	"bill_id" int4 NOT NULL,
	CONSTRAINT "wp_user_address_book_id_pkey" PRIMARY KEY("id")
);

COMMENT ON TABLE "wp_user_address_book" IS '用户 ship bill 地址簿';

COMMENT ON COLUMN "wp_user_address_book"."id" IS '自增长Id';

DROP TABLE IF EXISTS "wp_order_ship_bill" CASCADE;

CREATE TABLE "wp_order_ship_bill" (
	"id" SERIAL NOT NULL,
	"ship_company" varchar(50) NOT NULL DEFAULT '',
	"ship_attention" varchar(50) NOT NULL DEFAULT '',
	"ship_address" varchar(50) NOT NULL DEFAULT '',
	"ship_address2" varchar(50) NOT NULL DEFAULT '',
	"ship_address3" varchar(50) NOT NULL DEFAULT '',
	"ship_city_town" varchar(50) NOT NULL DEFAULT '',
	"ship_state" varchar(50) NOT NULL DEFAULT '',
	"ship_zip_code" varchar(50) NOT NULL DEFAULT '',
	"ship_country" varchar(50) NOT NULL DEFAULT '',
	"ship_phone" varchar(50) NOT NULL DEFAULT '',
	"ship_fax" varchar(50) NOT NULL DEFAULT '',
	"ship_email" varchar(255) NOT NULL DEFAULT '',
	"ship_remark" varchar(500) NOT NULL DEFAULT '',
	"bill_company" varchar(50) NOT NULL DEFAULT '',
	"bill_attention" varchar(50) NOT NULL DEFAULT '',
	"bill_address" varchar(50) NOT NULL DEFAULT '',
	"bill_address2" varchar(50) NOT NULL DEFAULT '',
	"bill_address3" varchar(50) NOT NULL DEFAULT '',
	"bill_city_town" varchar(50) NOT NULL DEFAULT '',
	"bill_state" varchar(50) NOT NULL DEFAULT '',
	"bill_zip_code" varchar(50) NOT NULL DEFAULT '',
	"bill_country" varchar(50) NOT NULL DEFAULT '',
	"bill_phone" varchar(50) NOT NULL DEFAULT '',
	"bill_fax" varchar(50) NOT NULL DEFAULT '',
	"bill_email" varchar(255) NOT NULL DEFAULT '',
	"bill_remark" varchar(500) NOT NULL DEFAULT '',
	"created_time" timestamp NOT NULL DEFAULT now(),
	CONSTRAINT "wp_order_ship_bill_id_pkey" PRIMARY KEY("id")
);

DROP TABLE IF EXISTS "wp_graphic_tag_item" CASCADE;

CREATE TABLE "wp_graphic_tag_item" (
	"id" SERIAL NOT NULL,
	"category_id" int4 NOT NULL DEFAULT 0,
	"name" varchar(50) NOT NULL DEFAULT '',
	"code" varchar(50) NOT NULL DEFAULT '',
	"size" varchar(50) NOT NULL DEFAULT '',
	"preview_image" varchar(255) NOT NULL DEFAULT '',
	"desc" varchar(255) NOT NULL DEFAULT '',
	"surcharge" varchar NOT NULL DEFAULT '0',
	"lead_time" varchar(255) NOT NULL DEFAULT '',
	"sort_no" int4 NOT NULL DEFAULT 1,
	"active" int2 NOT NULL DEFAULT 0,
	"created_time" timestamp NOT NULL DEFAULT now(),
	CONSTRAINT "wp_graphic_tag_item_id_pkey" PRIMARY KEY("id"),
	CONSTRAINT "wp_graphic_tag_code_key" UNIQUE("code")
);

COMMENT ON COLUMN "wp_graphic_tag_item"."surcharge" IS 'below moq surcharge';

COMMENT ON COLUMN "wp_graphic_tag_item"."active" IS '0可用 1不可用';

DROP TABLE IF EXISTS "wp_size_color" CASCADE;

CREATE TABLE "wp_size_color" (
	"id" SERIAL NOT NULL,
	"size" varchar(50) NOT NULL DEFAULT '',
	"pms_color" varchar(50) NOT NULL DEFAULT '',
	"version" varchar(50) NOT NULL DEFAULT '',
	"red" int2 NOT NULL DEFAULT 0,
	"green" int2 NOT NULL DEFAULT 0,
	"blue" int2 NOT NULL DEFAULT 0,
	"size_type" varchar(50) NOT NULL DEFAULT '',
	"sort_no" int4 NOT NULL DEFAULT 1,
	"created_time" timestamp NOT NULL DEFAULT now(),
	CONSTRAINT "wp_size_color_id_pkey" PRIMARY KEY("id"),
	CONSTRAINT "wp_size_color_size_key" UNIQUE("size")
);

DROP TABLE IF EXISTS "wp_graphic_tag_category" CASCADE;

CREATE TABLE "wp_graphic_tag_category" (
	"id" SERIAL NOT NULL,
	"name" varchar(50) NOT NULL DEFAULT '',
	"display_name" varchar(50) NOT NULL DEFAULT '',
	"active" int2 NOT NULL DEFAULT 0,
	"created_time" timestamp NOT NULL DEFAULT now(),
	CONSTRAINT "wp_graphic_tag_category_id_pkey" PRIMARY KEY("id")
);

DROP TABLE IF EXISTS "wp_graphic_tag_item_detail" CASCADE;

CREATE TABLE "wp_graphic_tag_item_detail" (
	"id" SERIAL NOT NULL,
	"item_id" int4 NOT NULL,
	"tier_qty" int4 NOT NULL DEFAULT 1,
	"price" varchar(50) NOT NULL DEFAULT '0',
	"uom_name" varchar(50) NOT NULL DEFAULT '',
	"uom_qty" int4 NOT NULL DEFAULT 1,
	"round_qty" int4 NOT NULL DEFAULT 1,
	"active" int2 NOT NULL DEFAULT 0,
	"created_time" timestamp NOT NULL DEFAULT now(),
	CONSTRAINT "wp_graphic_tag_item_detail_id_pkey" PRIMARY KEY("id")
);

COMMENT ON COLUMN "wp_graphic_tag_item_detail"."tier_qty" IS 'tier level qty';

COMMENT ON COLUMN "wp_graphic_tag_item_detail"."uom_name" IS 'name of units of measurement';

COMMENT ON COLUMN "wp_graphic_tag_item_detail"."uom_qty" IS 'qty of units of measurement';

COMMENT ON COLUMN "wp_graphic_tag_item_detail"."round_qty" IS 'qty to round';


ALTER TABLE "wp_user" ADD CONSTRAINT "wp_user_print_shop_id_fkey" FOREIGN KEY ("print_shop_id")
	REFERENCES "wp_print_shop"("id")
	MATCH SIMPLE
	ON DELETE NO ACTION
	ON UPDATE NO ACTION
	NOT DEFERRABLE;

ALTER TABLE "wp_group_permission" ADD CONSTRAINT "wp_group_permission_permission_id_fkey" FOREIGN KEY ("permission_id")
	REFERENCES "wp_permission"("id")
	MATCH SIMPLE
	ON DELETE NO ACTION
	ON UPDATE NO ACTION
	NOT DEFERRABLE;

ALTER TABLE "wp_group_permission" ADD CONSTRAINT "wp_group_permission_group_id_fkey" FOREIGN KEY ("group_id")
	REFERENCES "wp_group"("id")
	MATCH SIMPLE
	ON DELETE NO ACTION
	ON UPDATE NO ACTION
	NOT DEFERRABLE;

ALTER TABLE "wp_user_group" ADD CONSTRAINT "wp_user_group_group_id_fkey" FOREIGN KEY ("group_id")
	REFERENCES "wp_group"("id")
	MATCH SIMPLE
	ON DELETE NO ACTION
	ON UPDATE NO ACTION
	NOT DEFERRABLE;

ALTER TABLE "wp_user_group" ADD CONSTRAINT "wp_user_group_user_id_fkey" FOREIGN KEY ("user_id")
	REFERENCES "wp_user"("id")
	MATCH SIMPLE
	ON DELETE NO ACTION
	ON UPDATE NO ACTION
	NOT DEFERRABLE;

ALTER TABLE "wp_order" ADD CONSTRAINT "wp_order_created_id_fkey" FOREIGN KEY ("created_id")
	REFERENCES "wp_user"("id")
	MATCH SIMPLE
	ON DELETE NO ACTION
	ON UPDATE NO ACTION
	NOT DEFERRABLE;

ALTER TABLE "wp_order" ADD CONSTRAINT "wp_order_modified_id_fkey" FOREIGN KEY ("modified_id")
	REFERENCES "wp_user"("id")
	MATCH SIMPLE
	ON DELETE NO ACTION
	ON UPDATE NO ACTION
	NOT DEFERRABLE;

ALTER TABLE "wp_order" ADD CONSTRAINT "wp_order_print_shop_id_fkey" FOREIGN KEY ("print_shop_id")
	REFERENCES "wp_print_shop"("id")
	MATCH SIMPLE
	ON DELETE NO ACTION
	ON UPDATE NO ACTION
	NOT DEFERRABLE;

ALTER TABLE "wp_order" ADD CONSTRAINT "wp_order_order_ship_bill_id_fkey" FOREIGN KEY ("order_ship_bill_id")
	REFERENCES "wp_order_ship_bill"("id")
	MATCH SIMPLE
	ON DELETE NO ACTION
	ON UPDATE NO ACTION
	NOT DEFERRABLE;

ALTER TABLE "wp_order_detail" ADD CONSTRAINT "wp_order_detail_format_id_fkey" FOREIGN KEY ("format_id")
	REFERENCES "wp_format"("id")
	MATCH SIMPLE
	ON DELETE NO ACTION
	ON UPDATE NO ACTION
	NOT DEFERRABLE;

ALTER TABLE "wp_order_detail" ADD CONSTRAINT "wp_order_detail_order_id_fkey" FOREIGN KEY ("order_id")
	REFERENCES "wp_order"("id")
	MATCH SIMPLE
	ON DELETE NO ACTION
	ON UPDATE NO ACTION
	NOT DEFERRABLE;

ALTER TABLE "wp_print_history" ADD CONSTRAINT "wp_print_history_order_detail_id_fkey" FOREIGN KEY ("order_detail_id")
	REFERENCES "wp_order_detail"("id")
	MATCH SIMPLE
	ON DELETE NO ACTION
	ON UPDATE NO ACTION
	NOT DEFERRABLE;

ALTER TABLE "wp_user_address_book" ADD CONSTRAINT "wp_user_address_book_ship_id_fkey" FOREIGN KEY ("ship_id")
	REFERENCES "wp_ship_bill"("id")
	MATCH SIMPLE
	ON DELETE NO ACTION
	ON UPDATE NO ACTION
	NOT DEFERRABLE;

ALTER TABLE "wp_user_address_book" ADD CONSTRAINT "wp_user_address_book_bill_id_fkey" FOREIGN KEY ("bill_id")
	REFERENCES "wp_ship_bill"("id")
	MATCH SIMPLE
	ON DELETE NO ACTION
	ON UPDATE NO ACTION
	NOT DEFERRABLE;

ALTER TABLE "wp_user_address_book" ADD CONSTRAINT "wp_user_address_book_user_id_fkey" FOREIGN KEY ("user_id")
	REFERENCES "wp_user"("id")
	MATCH SIMPLE
	ON DELETE NO ACTION
	ON UPDATE NO ACTION
	NOT DEFERRABLE;

ALTER TABLE "wp_graphic_tag_item_detail" ADD CONSTRAINT "wp_graphic_tag_item_detail_item_id_fkey" FOREIGN KEY ("item_id")
	REFERENCES "wp_graphic_tag_item"("id")
	MATCH SIMPLE
	ON DELETE NO ACTION
	ON UPDATE NO ACTION
	NOT DEFERRABLE;


CREATE OR REPLACE VIEW "view_order" AS
	SELECT
	o.id, o.job_no, o.po_no, o.type, o.vendor_code, o.remark,
	o.completion_date, o.status, o.so_no, o.created_time,
	u.display_name as user_name,
	s.display_name as print_shop_name,
	COUNT (d.order_id) AS details_count,
	SUM (d.qty) AS tot_qty
FROM
	wp_order o
LEFT OUTER JOIN wp_user u ON o.created_id = u.id
LEFT OUTER JOIN wp_print_shop s ON o.print_shop_id = s.id
LEFT OUTER JOIN wp_order_detail d ON d.order_id = o.id
GROUP BY
	o.id, o.job_no, o.po_no, o.type, o.vendor_code, o.remark,
	o.completion_date, o.status, o.so_no, o.created_time,
	u.display_name,
	s.display_name;

COMMENT ON VIEW "view_order" IS 'search order results';




