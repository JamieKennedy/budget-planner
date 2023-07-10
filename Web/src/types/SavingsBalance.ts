import { z } from "zod";

export type TSavingsBalance = z.infer<typeof SavingsBalanceSchema>;
export type TSavingsBalanceCreate = z.infer<typeof SavingsBalanceCreateSchema>;

export const SavingsBalanceSchema = z.object({
    savingsBalanceId: z.string(),
    savingsId: z.string(),
    balance: z.number(),
    created: z.coerce.date(),
});

export const SavingsBalanceCreateSchema = z.object({
    balance: z.number(),
});
