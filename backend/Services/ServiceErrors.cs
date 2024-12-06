using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Services {
    public delegate void NotFountHandler(int id);
    public delegate void ConnectionErrorHandler();

    public class NotFoundException : Exception {
        public NotFoundException(int id)
            : base($"Resource with ID {id} was not found.") {
        }
    }

    public class ConnectionErrorException : Exception {
        public ConnectionErrorException()
            : base("A connection error occurred.") {
        }
    }
}
