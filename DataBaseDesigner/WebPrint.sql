-- =============================================================================
-- Diagram Name: QueueSystem
-- Created on: 2014/6/9 15:57:27
-- Diagram Version: 
-- =============================================================================


CREATE TABLE "wp_window" (
	"id" SERIAL NOT NULL,
	"number" varchar(50) NOT NULL,
	"create_time" timestamp NOT NULL DEFAULT now(),
	PRIMARY KEY("id"),
	CONSTRAINT "wp_window_unique_number_unique_key" UNIQUE("number")
);

CREATE TABLE "wp_queue" (
	"id" SERIAL NOT NULL,
	"jno" varchar(50) NOT NULL,
	"number" varchar(50) NOT NULL,
	"create_time" timestamp NOT NULL DEFAULT now(),
	PRIMARY KEY("id")
);

CREATE TABLE "wp_number" (
	"id" SERIAL NOT NULL,
	"prefix" varchar(50) DEFAULT '',
	"last_number" int4 NOT NULL DEFAULT 1,
	"create_time" timestamp NOT NULL DEFAULT now(),
	"update_time" timestamp NOT NULL DEFAULT now(),
	PRIMARY KEY("id")
);

CREATE TABLE "wp_window_queue" (
	"id" SERIAL NOT NULL,
	"jno" varchar(50) NOT NULL,
	"number" varchar(50) NOT NULL,
	"queue_time" timestamp NOT NULL,
	"window_id" int8 NOT NULL,
	"user_id" int4 NOT NULL,
	"create_time" timestamp NOT NULL DEFAULT now(),
	PRIMARY KEY("id"),
	CONSTRAINT "wp_window_queue_jno_unique_key" UNIQUE("jno")
);

CREATE TABLE "wp_window_history" (
	"id" SERIAL NOT NULL,
	"jno" varchar(50) NOT NULL,
	"number" varchar(50) NOT NULL,
	"queue_time" timestamp NOT NULL,
	"window_id" int8 NOT NULL,
	"window_queue_time" timestamp NOT NULL,
	"user_id" int4 NOT NULL,
	"create_time" timestamp NOT NULL DEFAULT now(),
	PRIMARY KEY("id")
);

CREATE TABLE "wp_user" (
	"id" SERIAL NOT NULL,
	"name" varchar(50) NOT NULL,
	"password" varchar(50) NOT NULL,
	"type" varchar(50) NOT NULL DEFAULT '',
	"create_time" timestamp NOT NULL DEFAULT now(),
	"active" int4 NOT NULL DEFAULT 1,
	PRIMARY KEY("id"),
	CONSTRAINT "wp_user_name_unique_key" UNIQUE("name")
);


ALTER TABLE "wp_window_queue" ADD CONSTRAINT "wp_window_queue_window_id_reference_to_wp_window_id" FOREIGN KEY ("window_id")
	REFERENCES "wp_window"("id")
	MATCH SIMPLE
	ON DELETE NO ACTION
	ON UPDATE NO ACTION
	NOT DEFERRABLE;

ALTER TABLE "wp_window_queue" ADD CONSTRAINT "wp_window_queue_user_id_reference_wp_user_id" FOREIGN KEY ("user_id")
	REFERENCES "wp_user"("id")
	MATCH SIMPLE
	ON DELETE NO ACTION
	ON UPDATE NO ACTION
	NOT DEFERRABLE;

ALTER TABLE "wp_window_history" ADD CONSTRAINT "wp_window_history_window_id_reference_to_wp_window_id" FOREIGN KEY ("window_id")
	REFERENCES "wp_window"("id")
	MATCH SIMPLE
	ON DELETE NO ACTION
	ON UPDATE NO ACTION
	NOT DEFERRABLE;

ALTER TABLE "wp_window_history" ADD CONSTRAINT "wp_window_history_user_id_reference_wp_user_id" FOREIGN KEY ("user_id")
	REFERENCES "wp_user"("id")
	MATCH SIMPLE
	ON DELETE NO ACTION
	ON UPDATE NO ACTION
	NOT DEFERRABLE;


