import { z } from "zod";
import { SavingsBalanceSchema } from "./SavingsBalance";

export type TSavings = z.infer<typeof SavingsSchema>;

export const SavingsSchema = z.object({
    savingsId: z.string(),
    userId: z.string(),
    name: z.string(),
    description: z.string(),
    goal: z.number(),
    goalDate: z.coerce.date().nullable(),
    lastModified: z.coerce.date(),
    created: z.coerce.date(),
    savingsBalances: z.array(SavingsBalanceSchema),
});
