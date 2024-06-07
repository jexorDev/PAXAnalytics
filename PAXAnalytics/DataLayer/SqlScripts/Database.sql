CREATE TABLE public.slices (
	"date" timestamp without time zone NOT NULL,
	pax_departing smallint NULL,
	planes_departing smallint NULL,
	pax_arriving smallint NULL,
	planes_arriving smallint NULL,
	CONSTRAINT slices_pk PRIMARY KEY ("date")
);
CREATE INDEX slices_date_idx ON public.slices ("date");