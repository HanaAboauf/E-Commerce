using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Shared.CommonResult
{
    // thsi class suitable for operations that do not return a value but indicate success or failure
    public class Result
    {
        private readonly List<Error> _errors =[];
        public IReadOnlyList<Error> Errors => _errors;
        public bool IsSuccess =>_errors.Count == 0;
        public bool IsFail => !IsSuccess;

        #region Constructors
        protected Result()
        {
        }

        protected Result(List<Error> errors)
        {
            _errors.AddRange(errors);
        }
        protected Result(Error error)
        {
            _errors.Add(error);
        }
        #endregion

        #region Methods

        public static Result Ok()=> new Result();
        public static Result Fail(Error error) => new Result(error);
        public static Result Fail(List<Error> errors) => new Result(errors);

        #endregion

    }

    // This class is suitable for operations that return a value upon success
    public class Result<TValue> : Result
    {
        private readonly TValue _Value;
        public TValue Value => IsSuccess ? _Value : throw new InvalidOperationException("Cannot access the value of a failed result.");
        #region Constructors
        private Result(TValue value)
        {
            _Value = value;
        }
        private Result(List<Error> errors) : base(errors)
        {
            _Value = default!;

        }
        private Result(Error error) : base(error)
        {
            _Value = default!;
        }
        #endregion
        #region Methods
        public static Result<TValue> Ok(TValue value) => new(value);
        public static new Result<TValue> Fail(Error error) => new(error);
        public static new Result<TValue> Fail(List<Error> errors) => new(errors);

        public static implicit operator Result<TValue>(TValue value) => Ok(value);
        public static implicit operator Result<TValue>(Error error) => Fail(error);
        public static implicit operator Result<TValue>(List<Error> errors) => Fail(errors);
        #endregion
    }
}
