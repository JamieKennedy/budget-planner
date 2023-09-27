import { z } from 'zod';
import { SavingsBalanceSchema } from './SavingsBalance';

export type TSavings = z.infer<typeof SavingsSchema>;
export type TSavingsCreate = z.infer<typeof SavingsCreateSchema>;
export type TSavingsEdit = z.infer<typeof SavingsEditSchema>;

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

export const SavingsCreateSchema = z.object({
    userId: z.string(),
    name: z.string(),
    description: z.string().optional(),
    goal: z.coerce.number(),
    goalDate: z.coerce.date().optional(),
});

export const SavingsEditSchema = z.object({
    userId: z.string(),
    savingsId: z.string(),
    name: z.string(),
    description: z.string().optional(),
    goal: z.coerce.number(),
    goalDate: z.coerce.date().nullable(),
});
