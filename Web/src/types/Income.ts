import { Occurrence, OccurrsOn } from './Enum';

import { z } from 'zod';

export type TIncome = z.infer<typeof IncomeSchema>;
export const IncomeSchema = z.object({
    id: z.string(),
    userId: z.string(),
    accountId: z.string(),
    Name: z.string(),
    Amount: z.number(),
    Occurrence: Occurrence,
    OccurrsOn: OccurrsOn,
    CustomOccurrsOn: z.number().nullish(),
    Created: z.coerce.date(),
    LastModified: z.coerce.date(),
});

export type TCreateIncome = z.infer<typeof CreateIncomeSchema>;
export const CreateIncomeSchema = z.object({
    accountId: z.string(),
    name: z.string(),
    amount: z.number(),
    Occurrence: Occurrence,
    OccurrsOn: OccurrsOn,
    CustomOccurrsOn: z.number().nullish(),
});

export type TUpdateIncome = z.infer<typeof UpdateIncomeSchema>;
export const UpdateIncomeSchema = z.object({
    accountId: z.string(),
    name: z.string().nullish(),
    amount: z.number().nullish(),
    Occurrence: Occurrence.nullish(),
    OccurrsOn: OccurrsOn.nullish(),
    CustomOccurrsOn: z.number().nullish(),
});
