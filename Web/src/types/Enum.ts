import { z } from 'zod';

export type EQueryStatus = z.infer<typeof QueryStatus>;
export const QueryStatus = z.enum(['error', 'success', 'pending']);

export type EMutationStatus = z.infer<typeof MutationStatus>;
export const MutationStatus = z.enum(['error', 'success', 'pending', 'idle']);

export type ETrendDirection = z.infer<typeof TrendDirection>;
export const TrendDirection = z.enum(['Up', 'Down', 'Level']);

export type ETheme = z.infer<typeof Theme>;
export const Theme = z.enum(['Light', 'Dark']);

export type EOccurrence = z.infer<typeof Occurrence>;
export const Occurrence = z.enum(['OneOff', 'Daily', 'Weekly', 'Monthly', 'Quarterly', 'Yearly']);

export type EOccurrsOn = z.infer<typeof OccurrsOn>;
export const OccurrsOn = z.enum(['FirstOf', 'LastOf', 'FirstWorkingDay', 'LastWorkingDay', 'Custom']);
