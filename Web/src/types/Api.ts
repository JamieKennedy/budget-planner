import { z } from 'zod';

export type TAuthorizeRequest = z.infer<typeof AuthorizeRequestSchema>;
export type TErrorResponse = z.infer<typeof ErrorResponseSchema>;
export type TTokenPayload = z.infer<typeof TokenPayloadSchema>;

const AuthorizeRequestSchema = z.object({
    email: z.string().email(),
    password: z.string(),
    keepLoggedIn: z.boolean(),
});

export const ErrorResponseSchema = z.object({
    StatusCode: z.number(),
    Message: z.string(),
});

const TokenPayloadSchema = z.object({
    aud: z.string(),
    exp: z.number(),
    iat: z.number(),
    iss: z.number(),
    nbf: z.number(),
    Id: z.string(),
});

export type EResponseType = z.infer<typeof ResponseTypesSchema>;
const ResponseTypesSchema = z.enum(['Json', 'Text', 'Void']);

export type ERequestMethod = z.infer<typeof RequestMethod>;
const RequestMethod = z.enum(['GET', 'POST', 'PATCH', 'DELETE', 'PUT']);
