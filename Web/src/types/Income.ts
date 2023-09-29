import { Occurrence, OccurrsOn } from './Enum';

import { z } from 'zod';

export type TIncome = z.infer<typeof IncomeSchema>;
export const IncomeSchema = z.object({
    id: z.string(),
    userId: z.string(),
    accountId: z.string(),
    name: z.string(),
    amount: z.number(),
    occurrence: Occurrence,
    occurrsOn: OccurrsOn,
    customOccurrsOn: z.number().nullish(),
    created: z.coerce.date(),
    lastModified: z.coerce.date(),
});

export type TCreateIncome = z.infer<typeof CreateIncomeSchema>;
export const CreateIncomeSchema = z.object({
    accountId: z.string(),
    name: z.string(),
    amount: z.number(),
    occurrence: Occurrence,
    occurrsOn: OccurrsOn,
    customOccurrsOn: z.number().nullish(),
});

export type TUpdateIncome = z.infer<typeof UpdateIncomeSchema>;
export const UpdateIncomeSchema = z.object({
    accountId: z.string(),
    name: z.string().nullish(),
    amount: z.number().nullish(),
    occurrence: Occurrence.nullish(),
    occurrsOn: OccurrsOn.nullish(),
    customOccurrsOn: z.number().nullish(),
});
