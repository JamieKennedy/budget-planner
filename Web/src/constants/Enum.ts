import { z } from "zod";

export type EWidgetState = z.infer<typeof WidgetState>;
export const WidgetState = z.enum(["Loading", "Loaded", "Errored"]);
