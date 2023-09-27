using System;

namespace recados_api
{
    public class CriarConta
    {
        public string Username { get; set; }
        public string Senha { get; set; }
        public string Id { get; set; }


    };

    public class ValidatorNovaContaReturn
        {
            public string Status { get; set; }
            public string Message { get; set; }

        public static implicit operator ValidatorNovaContaReturn(CriarContaValidator v)
        {
            throw new NotImplementedException();
        }
    };

        
};


